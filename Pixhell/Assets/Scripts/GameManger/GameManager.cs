//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using InventoryClass;

public static class GameManager {
    // Accessable Player info for current run
    [SerializeField] static int maxArena;
    [SerializeField] static string runID;
    private static string path = Application.streamingAssetsPath;

    public static Inventory inventory;
    public static int coins;

    static void LoadPlayerData() {
        // Implement functionality that reads a file and places the data in the respective fields
    }

    static void SetMaxArena(int max) {
        maxArena = max;
    }

    static void UpdateCoins(int amount) {
        coins += amount;
    }

    static List<Item> getInventory() {
        return inventory.getList();
    }
}
 