using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ExitGame : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick() 
    {
        // Quit the game
        Application.Quit();

        // Specific to unity editor (# is a Unity thing)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}