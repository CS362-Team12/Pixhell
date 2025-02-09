// Works with the SelectRun scene to load existing runs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class LoadCharacterSaves : MonoBehaviour
{
    private int runCount = 0;
    private string path = Application.streamingAssetsPath;

    public GameObject buttonPrefab;
    public Transform panelContainer;

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

    void AddRunToPanel(string filePath) {
    try {
        using (StreamReader reader = new StreamReader(filePath)) {
            string firstLine = reader.ReadLine(); // Read the first line of the file

            // Instantiate a new button from the prefab
            GameObject newButton = Instantiate(buttonPrefab.gameObject, panelContainer);
            if (newButton == null) {
                Debug.LogError("Failed to instantiate buttonPrefab!");
                return;
            }

            // Get the button's text component
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText == null) {
                Debug.LogError("TextMeshProUGUI component not found in buttonPrefab!");
                return;
            }

            // Set the button's text
            buttonText.text = firstLine + filePath;

            // Get the Button component
            Button button = newButton.GetComponent<Button>();
            if (button == null) {
                Debug.LogError("Button component not found on buttonPrefab!");
                return;
            }

            // Add button functionality
            button.onClick.AddListener(() => EnterRun(filePath));

            // Positioning the button to prevent overlap
            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            if (buttonRect == null) {
                Debug.LogError("RectTransform component not found on buttonPrefab!");
                return;
            }

            float buttonWidth = buttonRect.rect.width; // Get width of button prefab
            float spacing = 10f; // Space between buttons

            // Set button position
            buttonRect.anchoredPosition = new Vector2(runCount * (buttonWidth + spacing), 0);

            // Increment run count
            runCount++;
        }
    } 
    catch (Exception ex) {
        Debug.LogError("Error reading the file: " + ex.Message);
    }
}

    void GetRuns() {
        string generalPath = path + "/Runs";

        string[] allFiles = Directory.GetFiles(generalPath, "*.txt", SearchOption.TopDirectoryOnly);
        // Go through all run files in this directory
        // For each one, add to existing list of runs in the menu (some kind of scroll thing that shows runs you can click on
        
        foreach (string file in allFiles)
        {
                AddRunToPanel(file);
        }
        // In each file, read the top line (Character type)
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
            writer.WriteLine("Arena: 1\n");
            Debug.Log("New run created with filename: " + filePath);
        }
        AddRunToPanel(filePath);
    }

    void EnterRun(string filePath) {

        SceneManager.LoadScene("Lobby");
    }
}