// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class ItemShop : MonoBehaviour
{
    GameObject itemShopUI;
    bool shopShowing;

    public GameObject itemsPanel;
    int count;
    string filePath;

    int buttonSize;

    void Start() {
        itemShopUI = transform.Find("ItemShopUI").gameObject;
        shopShowing = false;
        itemShopUI.SetActive(false);
        count = 0;
        buttonSize = 500;
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
        count = 0;
        itemsPanel = new GameObject("ItemsPanel", typeof(RectTransform));
        itemsPanel.transform.SetParent(itemShopUI.transform);
        RectTransform panelRect = itemsPanel.GetComponent<RectTransform>();
        panelRect.anchoredPosition = new Vector2(0, 0);

        filePath = GameManager.inventory.itemInfoPath;
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
        GameObject newButton = new GameObject("ItemButton", typeof(RectTransform), typeof(Button), typeof(Image));
        newButton.transform.SetParent(itemsPanel.transform, false);
        RectTransform buttonRect = newButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(buttonSize, buttonSize);

        // Button image
        Image buttonBg = newButton.GetComponent<Image>();

        GameObject imageObject = new GameObject("ItemImage", typeof(RectTransform), typeof(Image));
        imageObject.transform.SetParent(newButton.transform, false);

        Image itemImage = imageObject.GetComponent<Image>();
        itemImage.rectTransform.sizeDelta = new Vector2(400, 400);

        // Load the image from file
        string path = Path.Combine(Application.streamingAssetsPath, item.imagePath);
        if (File.Exists(path))
        {
            byte[] imageBytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            itemImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        if (GameManager.inventory.hasItem(item)) 
        {
            Debug.Log("GREYSCALING");
            buttonBg.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => ViewItem(item));

        int columns = 2; // Number of items per row
        int row = count / columns;
        int column = count % columns == 0 ? -1 : 1;

        buttonRect.anchoredPosition = new Vector2(column * buttonSize * 0.75f, (-row + 1) * buttonSize);
        count++;
    }

    void ViewItem(Item item) 
    {
        Debug.Log("Viewing item " + item.name + "!");    
    }
}