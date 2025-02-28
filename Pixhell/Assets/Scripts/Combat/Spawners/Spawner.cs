using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using System.Linq;

public class Spawner : MonoBehaviour
{

    public GameObject tempTestPrefab;
    GameObject player;
    string sceneName;
    string waveDataFilePath;
    public bool spawning;
    int spawnersRunning;
    public int currentWave;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        spawning = false;
        spawnersRunning = 0;
        player = GameObject.FindWithTag("Player");
        sceneName = scene.name;
        waveDataFilePath = Path.Combine(Application.streamingAssetsPath, "Arenas", sceneName + ".csv");
        Debug.Log("Checking for enemies.");
        if (File.Exists(waveDataFilePath)) {
            spawning = true;
            currentWave = 1;
            StartCoroutine(RunSpawnScript());
        }
    }

    IEnumerator RunSpawnScript() {
        string[] lines = File.ReadAllLines(waveDataFilePath);
        while (spawning) {
            string[] waveLines = getWaveLines(lines);
            if (waveLines.Length == 0) { break; }
                
            foreach (string line in waveLines) {
                StartCoroutine(SpawnLine(line));
            }
            yield return new WaitUntil(IsNoEnemies);
            Debug.Log(currentWave);
            currentWave++;
        }
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
            Debug.Log(rootObj.name);
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
        for (int i = 0; i < count; i++) {
            SpawnEnemy(enemyType);
            yield return new WaitForSeconds(timeToSpawn / count);
        }
        spawnersRunning -= 1;
    }

    void SpawnEnemy(string enemyType) {
        //GameObject enemyPrefab = Resources.Load<GameObject>("Enemies/" + enemyType);
        GameObject enemyPrefab = tempTestPrefab;
        if (enemyPrefab != null) {
            Instantiate(enemyPrefab, player.transform.position + new Vector3(1f,1f,0f), Quaternion.identity);
        } else {
            Debug.LogError("Enemy prefab not found: " + enemyType);
        }
    }


    string[] getWaveLines(string[] allLines) {
        return allLines.Where(line => line.StartsWith(currentWave.ToString() + ",")).ToArray();
    }

    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
