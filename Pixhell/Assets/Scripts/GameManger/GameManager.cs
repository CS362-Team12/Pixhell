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
    [SerializeField] static string runIDPath;
    private static string path = Application.streamingAssetsPath;

    public static Inventory inventory = new Inventory();
    public static int coins;

    public static void LoadPlayerData(string filePath) {
        runIDPath = filePath;
        inventory.resetInventory();
        using (StreamReader reader = new StreamReader(filePath)) 
        {
            maxArena = int.Parse(reader.ReadLine().Split(" ")[1]);
            string itemLine = reader.ReadLine().Substring(6);
            Debug.Log("ITEMLINE: " + itemLine);
            string[] items = itemLine.Split(',');
            Debug.Log(items.Length);
            for (int i = 0; i < items.Length; i++) {
                string itemId = items[i];
                if (itemId.Length > 0) {
                    inventory.addItem(int.Parse(itemId));
                }
                
            }
        }
    }

    public static void LogPlayerData() {
        string dataString = $"Arena: {maxArena}, runID: {runIDPath}, InventorySize: {inventory.items.Count}";
        Debug.Log(dataString);
    }
}
 