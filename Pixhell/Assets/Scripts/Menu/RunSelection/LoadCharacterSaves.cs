// Works with the SelectRun scene to load existing runs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using TMPro;

public class LoadCharacterSaves : MonoBehaviour
{
    public int runCount = 0;
    private int buttonSpacing = 10;
    private string path = Application.streamingAssetsPath;

    public GameObject buttonPrefab;
    public RectTransform panelContainer;

    void Start()
    {
        Debug.Log(path);
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if(!Directory.Exists(path + "/Runs"))
        {
            Directory.CreateDirectory(path + "/Runs");
        }
        GetRuns();

    }

    void AddRunToPanel(string filePath) 
    {
        try 
        {
            using (StreamReader reader = new StreamReader(filePath)) 
            {
                string firstLine = reader.ReadLine(); // Read the first line of the file
                string secondLine = reader.ReadLine();
                string thirdLine = reader.ReadLine();

                // Instantiate a new button from the prefab
                GameObject newButton = Instantiate(buttonPrefab.gameObject, panelContainer);
                if (newButton == null) 
                {
                    Debug.LogError("Failed to instantiate buttonPrefab!");
                    return;
                }

                // Get the button's text component
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText == null) 
                {
                    Debug.LogError("TextMeshProUGUI component not found in buttonPrefab!");
                    return;
                }

                // Set the button's text
                buttonText.text = "CLICK TO LOAD LOBBY\n\n\n\nBUTTON DESIGN IN PROGRESS\n" + firstLine + "\n" + filePath + "\n" + secondLine + "\n" + thirdLine;
                Button button = newButton.GetComponent<Button>();
                // Add button functionality
                button.onClick.AddListener(() => EnterRun(filePath));

                RectTransform buttonRect = newButton.GetComponent<RectTransform>();

                // Set the button's pivot to the left
                buttonRect.pivot = new Vector2(0f, 0.5f); // Pivot at the left side (x = 0)
                buttonRect.anchorMin = new Vector2(0f, 0.5f); // Anchor at the left side
                buttonRect.anchorMax = new Vector2(0f, 0.5f); // Anchor at the left side

                float buttonWidth = buttonRect.rect.width; // Get width of button prefab

                // Set button position relative to the left side of the panel with spacing
                float buttonPositionX = (buttonWidth + buttonSpacing) * runCount; // Add spacing between buttons

                buttonRect.anchoredPosition = new Vector2(buttonPositionX, 0); // Add gap and position the button

                runCount++; // Increment the run count after positioning each button
            }
        } 
        catch (Exception ex) 
        {
            Debug.LogError("Error reading the file: " + ex.Message);
        }
    }

    void GetRuns() {
        string generalPath = path + "/Runs";

        string[] allFiles = Directory.GetFiles(generalPath, "*.txt", SearchOption.TopDirectoryOnly);
        runCount = 0;
        // Sort by last edit time
        allFiles = allFiles.OrderByDescending(file => 
        {
            DateTime lastEdit = File.GetLastWriteTime(file);
            runCount++;
            return lastEdit;
        }).ToArray();

        panelContainer.pivot = new Vector2(0f, 0.5f);  // Pivot on the left
        panelContainer.anchorMin = new Vector2(0f, 0.5f); // Left side anchored
        panelContainer.anchorMax = new Vector2(0f, 0.5f); // Left side anchored
        float width = runCount * (buttonSpacing + GetPrefabButtonWidth());
        panelContainer.sizeDelta = new Vector2(width, panelContainer.sizeDelta.y);

        // Go through all run files in this directory
        // For each one, add to existing list of runs in the menu (some kind of scroll thing that shows runs you can click on
        runCount = 0;
        foreach (string file in allFiles)
        {
            AddRunToPanel(file);
        }
    }

    public void MakeFreshRun() {
        // Used to generate a new, fresh run
        // Called when user presses +New button
        // Add the new run to the existing panel
        string generalPath = path + "/Runs";

        string[] allFiles = Directory.GetFiles(generalPath, "*", SearchOption.TopDirectoryOnly);
        // Go through all run files in this directory
        // For each one, add to existing list of runs in the menu (some kind of scroll thing that shows runs you can click on
        string randomString = Pixhell.HelperFunctions.HelperFunctions.GenerateRandomString(10) + ".txt";
        // Ensure that the filename doesn't already exist
        while (Array.Exists(allFiles, file => Path.GetFileName(file) == randomString))
        {
            randomString = Pixhell.HelperFunctions.HelperFunctions.GenerateRandomString(10) + ".txt";  // Regenerate filename if it exists
        }

        // Create the new file with the unique filename
        string filePath = generalPath + "/" + randomString;
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Arena: 1");
            writer.WriteLine("Coins: 0");
            writer.WriteLine("Items:");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EnterRun(string filePath) {
        GameManager.LoadPlayerData(filePath);
        SceneManager.LoadScene("Limbo");
    }

    float GetPrefabButtonWidth()
    {
        // Ugly way of doing it but easier than manually updating changes
        GameObject tempButton = Instantiate(buttonPrefab); // Create a temporary button
        RectTransform rectTransform = tempButton.GetComponent<RectTransform>();
        float width = rectTransform.sizeDelta.x;
        Destroy(tempButton);

        return width;
    }
}