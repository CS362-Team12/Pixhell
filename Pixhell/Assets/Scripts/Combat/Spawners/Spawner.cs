using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{

    public float minSpawnDistance = 10f;
    GameObject player;
    string sceneName;
    string waveDataFilePath;
    public bool spawning;
    int spawnersRunning;
    public int currentWave;
    public bool scriptCompleted;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        StopAllCoroutines();
        spawning = false;
        scriptCompleted = false;
        spawnersRunning = 0;
        player = GameObject.FindWithTag("Player");
        sceneName = scene.name;
        waveDataFilePath = Path.Combine(Application.streamingAssetsPath, "Arenas", sceneName + ".csv");
        Debug.Log("Checking for enemies.");
        if (File.Exists(waveDataFilePath)) {
            spawning = true;
            currentWave = 1;
            StartCoroutine(RunSpawnScript());
            StartCoroutine(ArenaCompleted());
        }
    }

    IEnumerator ArenaCompleted()
    {
        while (!scriptCompleted)
        {
            yield return null;
        }
        GameWin win = transform.parent.Find("WinScreen").GetComponent<GameWin>();
        win.OnGameWin();
    }

    IEnumerator RunSpawnScript() {
        string[] lines = File.ReadAllLines(waveDataFilePath);
        while (spawning) {
            string[] waveLines = GetWaveLines(lines);
            if (waveLines.Length == 0) { break; }
                
            foreach (string line in waveLines) {
                StartCoroutine(SpawnLine(line));
            }
            yield return new WaitUntil(IsNoEnemies);
            Debug.Log(currentWave);
            currentWave++;
        }
        scriptCompleted = true;
    }

    bool IsNoEnemies()
    {
        if (spawnersRunning > 0) {
            return false;
        }
        Scene targetScene = SceneManager.GetSceneByName(sceneName);
        GameObject[] rootObjects = targetScene.GetRootGameObjects();
        foreach (var rootObj in rootObjects)
        {
            if (rootObj.CompareTag("Enemy"))
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnLine(string line) {
        spawnersRunning += 1;
        string[] parts = line.Split(',');
        string enemyType = parts[1];
        int count = int.Parse(parts[2]);
        float timeToSpawn = float.Parse(parts[3]);
        float delay = float.Parse(parts[4]);
        yield return new WaitForSeconds(delay);

        // Fence Post: Spawn an enemy, then enter the waiting lool
        // 3 enemies spawn: spawn, wait, spawn, wait, spawn
        SpawnEnemy(enemyType);
        for (int i = 0; i < count-1; i++) {
            yield return new WaitForSeconds(timeToSpawn / (count - 1));
            SpawnEnemy(enemyType);
        }
        spawnersRunning -= 1;
    }

    void SpawnEnemy(string enemyType) {
        GameObject enemyPrefab = Resources.Load<GameObject>("Enemies/" + enemyType + "Object");
        if (enemyPrefab != null) {
            Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        } else {
            Debug.LogError("Enemy prefab not found: " + enemyType);
        }
    }

    Vector3 GetRandomSpawnPosition() 
    {
        float xMod = 0f;
        float yMod = 0f;
        while (Mathf.Sqrt(xMod * xMod + yMod * yMod) < 0.4f)
        {
            xMod = (UnityEngine.Random.value - 0.5f) * 2f;
            yMod = (UnityEngine.Random.value - 0.5f) * 2f;
        }
        
        return player.transform.position + new Vector3(xMod * minSpawnDistance, yMod * minSpawnDistance, 0);
    }

    string[] GetWaveLines(string[] allLines) {
        List<string> filteredLines = new List<string>();
        foreach (var line in allLines)
        {
            if (line.StartsWith(currentWave.ToString() + ","))
            {
                filteredLines.Add(line);
            }
        }
        return filteredLines.ToArray();
    }

    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
