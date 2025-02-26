// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Collections;

public class ItemShop : MonoBehaviour
{
    GameObject itemShopUI;
    public bool shopShowing;

    GameObject itemsPanel;
    public GameObject infoPanel;
    public int count;
    string filePath;
    RectTransform panelRect;

    int buttonSize;
    int spacing;
    public int pageSpacing;

    private Color ownedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    private Color notOwnedColor = new Color(1f, 1f, 1f, 1f);

    void Start() {
        itemShopUI = transform.Find("ItemShopUI").gameObject;
        shopShowing = false;
        itemShopUI.SetActive(false);
        count = 0;
        buttonSize = 500;
        spacing = 100;
        pageSpacing = 1000;
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
        infoPanel.SetActive(false);
        itemShopUI.SetActive(shopShowing);  // Show/hide the shop
        Time.timeScale = !shopShowing ? 1f : 0f;  // Freeze gameplay time when shop open
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) { // Ensure that the scene has a player obj
            player.GetComponent<PlayerController>().enabled = !shopShowing ? true: false;
        }
        if (shopShowing) {
            LoadShop();
        }
        
    }

    void LoadShop() 
    {
        Destroy(itemsPanel);
        count = 0;
        itemsPanel = new GameObject("ItemsPanel", typeof(RectTransform));
        itemsPanel.transform.SetParent(itemShopUI.transform);
        panelRect = itemsPanel.GetComponent<RectTransform>();
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
            buttonBg.color = ownedColor;
        }
        else {
            buttonBg.color = notOwnedColor;
        }

        // Create the cost text
        GameObject costTextObject = new GameObject("CostText", typeof(RectTransform), typeof(TextMeshProUGUI));
        costTextObject.transform.SetParent(newButton.transform, false);

        // Set up the TextMeshProUGUI component
        TextMeshProUGUI costText = costTextObject.GetComponent<TextMeshProUGUI>();
        costText.text = "$" + item.cost.ToString(); // Set the cost as text
        costText.fontSize = 64;
        costText.color = Color.black;
        costText.alignment = TextAlignmentOptions.TopRight; // Align to the top-right corner

        // Adjust the RectTransform for the cost text to position it in the top-right corner
        RectTransform costRect = costTextObject.GetComponent<RectTransform>();
        costRect.sizeDelta = new Vector2(150, 30); // Set the size of the cost text box
        costRect.anchoredPosition = new Vector2(0.5f * buttonSize - 90, 0.5f * buttonSize - 20); // Position in top-right corner


        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => ViewItem(item));

        int page = count / 6;
        int columns = 2; // Number of items per row
        int row = (count - (6 * page)) / columns;
        int column = count % columns == 0 ? -1 : 1;
        

        buttonRect.anchoredPosition = new Vector2(column * buttonSize * 0.75f, ((-row + 1) * (buttonSize + spacing)) - (pageSpacing * page * 2));
        count++;
    }

    void ViewItem(Item item) 
    {
        infoPanel.SetActive(true);
        Image img = infoPanel.transform.Find("InfoImage").GetComponent<Image>();
        string path = Path.Combine(Application.streamingAssetsPath, item.imagePath);
        if (File.Exists(path)) {
            byte[] imageBytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        TextMeshProUGUI text = infoPanel.transform.Find("InfoText").GetComponent<TextMeshProUGUI>();
        text.text = item.name + "\n\n" + item.description + "\n\n";
        Button purchaseButton = infoPanel.transform.Find("PurchaseButton").GetComponent<Button>(); 
        if (GameManager.inventory.hasItem(item)) {
            text.text += "OWNED";
            TextMeshProUGUI buttonText = purchaseButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Sell";
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(() => SellItem(item));
        }
        else {
            text.text += "Cost: " + item.cost;
            TextMeshProUGUI buttonText = purchaseButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Purchase";
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(() => PurchaseItem(item));
        }
    }

    void PurchaseItem(Item item) {
        if (item.cost <= GameManager.coins) {
            GameManager.coins -= item.cost;
            GameManager.inventory.addItem(item);
            float currentY = panelRect.anchoredPosition.y;
            ToggleUI();
            ToggleUI();
            ViewItem(item);
            panelRect.anchoredPosition = new Vector2(panelRect.anchoredPosition.x, currentY);
            GameManager.SavePlayerData();
        }
        else {
            StartCoroutine(ShowWarningText("You do not have enough coins to purchase this item!", 1.5f, itemShopUI));
        }
    }

    void SellItem(Item item) {
        GameManager.coins += item.cost;
        GameManager.inventory.removeItem(item);
        float currentY = panelRect.anchoredPosition.y;
        ToggleUI();
        ToggleUI();
        ViewItem(item);
        panelRect.anchoredPosition = new Vector2(panelRect.anchoredPosition.x, currentY);
        GameManager.SavePlayerData();
    }

    private IEnumerator ShowWarningText(string message, float duration, GameObject canvas)
    {
        // Create new Text GameObject
        GameObject textObj = new GameObject("WarningText");
        textObj.transform.SetParent(canvas.transform, false); // Attach to canvas

        // Add Text component
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = message;
        textComponent.fontSize = 36;
        textComponent.color = Color.red;
        textComponent.alignment = TextAlignmentOptions.Center;
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(1000, 100);
        // Wait for duration and destroy
        yield return new WaitForSecondsRealtime(duration);
        Destroy(textObj);
    }
}