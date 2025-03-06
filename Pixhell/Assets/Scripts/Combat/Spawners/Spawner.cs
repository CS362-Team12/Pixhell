using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

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
    Tilemap spawnTilemap;

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
            GameObject grid = GameObject.FindWithTag("AllowSpawns");
            spawnTilemap = grid.GetComponentInChildren<Tilemap>();
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
        string[] parts = line.Split(',');
        string enemyType = parts[1];
        int count = int.Parse(parts[2]);
        float timeToSpawn = float.Parse(parts[3]);
        float delay = float.Parse(parts[4]);
        if (count > 0) {
        spawnersRunning += 1;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < count - 1; i++) {
            SpawnEnemy(enemyType);
            yield return new WaitForSeconds(timeToSpawn / count);
        }
        SpawnEnemy(enemyType);
        spawnersRunning -= 1;
        }
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
        int overflowCatcher = 80;
        while ((Mathf.Sqrt(xMod * xMod + yMod * yMod) < 0.4f) || !(OnTilemap(xMod, yMod)) && overflowCatcher > 0)
        {
            xMod = (UnityEngine.Random.value - 0.5f) * 2f;
            yMod = (UnityEngine.Random.value - 0.5f) * 2f;
            overflowCatcher -= 1;
        }
        
        return player.transform.position + new Vector3(xMod * minSpawnDistance, yMod * minSpawnDistance, 0);
    }

    bool OnTilemap(float xMod, float yMod)
    {
        Vector3 spawnPosition = player.transform.position + new Vector3(xMod * minSpawnDistance, yMod * minSpawnDistance, 0);
        Vector3Int posOnTilemap = spawnTilemap.WorldToCell(spawnPosition);
        TileBase tile = spawnTilemap.GetTile(posOnTilemap);

        return tile != null;
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
