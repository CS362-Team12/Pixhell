// Works with the SelectRun scene to load existing runs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

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
        // STILL BUGGY
        try
            {
                // Open the file and read the first line
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string firstLine = reader.ReadLine();  // Read the first line of the file

                // Instantiate a new button from the prefab
                GameObject newButton = Instantiate(buttonPrefab, panelContainer);

                // Get the button's text component
                Text buttonText = newButton.GetComponentInChildren<Text>();

                // Set the button's text to the first line of the file
                if (buttonText != null)
                {
                    buttonText.text = firstLine;
                }

                // Add functionality when the button is clicked
                Button button = newButton.GetComponent<Button>();
                button.onClick.AddListener(() => EnterRun(filePath));
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
            }
    }

    void GetRuns() {
        string generalPath = path + "/Runs";

        string[] allFiles = Directory.GetFiles(generalPath, "*", SearchOption.TopDirectoryOnly);
        // Go through all run files in this directory
        // For each one, add to existing list of runs in the menu (some kind of scroll thing that shows runs you can click on
        
        foreach (string file in allFiles)
        {
                AddRunToPanel(file);
        }
        // In each file, read the top line (Character type)
    }

    void MakeFreshRun() {
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

        SceneManager.LoadScene("Lobby")
    }
}