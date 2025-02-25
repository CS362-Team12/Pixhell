//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using InventoryClass;

public static class GameManager {
    // Accessable Player info for current run
    [SerializeField] static int maxArena;
    [SerializeField] static int coins;
    [SerializeField] static string runIDPath;
    private static string path = Application.streamingAssetsPath;

    public static Inventory inventory = new Inventory();

    public static void LoadPlayerData(string filePath) {
        runIDPath = filePath;
        inventory.resetInventory();
        using (StreamReader reader = new StreamReader(filePath)) 
        {
            // Read Arena
            maxArena = int.Parse(reader.ReadLine().Split(" ")[1]);

            // Read Coins
            string coinLine = reader.ReadLine();
            Debug.Log(coinLine);
            // Read Coins
            coins = int.Parse(coinLine.Split(" ")[1]);
            
            // Read Items
            string itemLine = reader.ReadLine().Substring(6);
            Debug.Log("ITEMLINE: " + itemLine);
            string[] items = itemLine.Split(',');
            Debug.Log("Item count: " + items.Length);
            for (int i = 0; i < items.Length; i++) {
                string itemId = items[i];
                Debug.Log("Current i for loop: " + i);
                if (itemId.Length > 0) {
                    Debug.Log("Added item" +  items.Length);
                    inventory.addItem(int.Parse(itemId));
                }
            
            }
        }
    }

    // Debugging function
    public static void LogPlayerData() {
        string dataString = $"Arena: {maxArena}, runID: {runIDPath}, InventorySize: {inventory.items.Count}";
        Debug.Log(dataString);
    }
}
 