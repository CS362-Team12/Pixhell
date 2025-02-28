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
    public int currentWave;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        spawning = false;
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
                StartCoroutine(SpawnWave(line));
            }
            yield return new WaitUntil(IsNoEnemies);
            currentWave++;
        }
    }

    bool IsNoEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    IEnumerator SpawnWave(string line) {
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
