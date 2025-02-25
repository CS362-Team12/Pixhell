// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;
using System;
using System.IO;


public class ItemShop : MonoBehaviour
{
    GameObject itemShopUI;
    bool shopShowing;

    GameObject itemsPanel;

    void Start() {
        itemShopUI = transform.Find("ItemShopUI").gameObject;
        shopShowing = false;
        itemShopUI.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!shopShowing) {
            ToggleUI();
        }
    }

    public void ToggleUI()
    // Toggles the shop menu
    {
        Debug.Log("Toggled Shop");
        shopShowing = !shopShowing;
        itemShopUI.SetActive(shopShowing);  // Show/hide the shop
        Time.timeScale = !shopShowing ? 1f : 0f;  // Freeze gameplay time when shop open
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) { // Ensure that the scene has a player obj
            player.GetComponent<PlayerController>().enabled = !shopShowing ? true: false;
        }
        if (shopShowing) {
            LoadShop();
        }
        else {

        }
        
    }

    void LoadShop() 
    {
        itemsPanel = new GameObject("ItemsPanel");
        itemsPanel.transform.SetParent(itemShopUI.transform);

        string filePath = GameManager.inventory.itemInfoPath;
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++) 
        {
            string line = lines[i];
            string[] columns = line.Split(',');
            string id = columns[0];
            Item item = GameManager.inventory.readItem(columns);
            AddItemToPanel(item);
        }
    }

    void AddItemToPanel(Item item) 
    {
        Debug.Log(item.id);
    }
}