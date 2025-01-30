
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadCharacterSave : MonoBehaviour
{
    void Start()
    {
        string path = "../../../Data";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + "/Saves");
        }

    }

    void GetRuns() {
        string generalPath = "../../../Data/Saves";
        // Go through all run files in this directory
        // For each one, add to existing list of runs in the menu (some kind of scroll thing that shows runs you can click on
        
        // In each file, read the top line (Character type)
    }
}