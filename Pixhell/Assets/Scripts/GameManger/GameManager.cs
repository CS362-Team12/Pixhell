//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using InventoryClass;

public class GameManager : MonoBehaviour {
    // Accessable Player info for current run
    [SerializeField] string sceneName;
    [SerializeField] int maxArena;
    [SerializeField] string runID;
    private string path = Application.streamingAssetsPath;

    public Inventory inventory;
    public int coins;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void LoadPlayerData() {
        // Implement functionality that reads a file and places the data in the respective fields
    }
}
