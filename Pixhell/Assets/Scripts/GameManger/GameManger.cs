//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public static class GameManager : MonoBehaviour {
    [SerializeField] string sceneName;
    [SerializeField] int maxArena;
    [SerializeField] string runID;
    private string path = Application.streamingAssetsPath;
    //[SerializeField] Items[] Inventory; // Used for accessing inventory items quickly
    static void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    static void LoadInventory() {
        // Implement functionality that reads a file and places the data in the respective fields
    }
}