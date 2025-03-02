//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using InventoryClass;
using System.Linq;

public static class GameManager {
    // Accessable Player info for current run
    public static int maxArena;
    public static int coins;
    public static string runIDPath;
    public static string path = Application.streamingAssetsPath;

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

    public static void SavePlayerData() {
        
        if (runIDPath != null) {
            using (StreamWriter writer = new StreamWriter(runIDPath))
            {
            writer.WriteLine("Arena: " + maxArena);
            writer.WriteLine("Coins: " + coins);
            string idList = string.Join(",", inventory.items.Select(item => item.id.ToString()).ToArray());
            writer.WriteLine("Items:" + idList);
            }
        }
    }

    // Debugging function
    public static void LogPlayerData() {
        string dataString = $"Arena: {maxArena}, runID: {runIDPath}, InventorySize: {inventory.items.Count}";
        Debug.Log(dataString);
    }
}
 