using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused;
    private string prevScene;



    void Start()
    {
        pauseMenuUI.SetActive(false);  // Initially hide the pause menu
        prevScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (prevScene != currentScene && isPaused == true) {
            TogglePause(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            bool isPaused;
        }
        prevScene = currentScene;

    }

    public void TogglePause(bool allowToggle=false)
    // Toggles the current pause setting
    // Takes a force parameter that will allow the toggle to happen even on menus where it should not be allowed
    {
        if ((SceneManager.GetActiveScene().name == "StartMenu" || SceneManager.GetActiveScene().name == "SelectRun") && !allowToggle) {
            return;
        }
        GameManager.LogPlayerData();

        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);  // Show/hide the pause menu
        Time.timeScale = isPaused ? 0f : 1f;  // Freeze gameplay time when paused
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) { // Ensure that the scene has a player obj
            player.GetComponent<PlayerController>().enabled = isPaused ? false: true;
            SceneButtonBehavior();
        }
    }

    void SceneButtonBehavior() 
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        if (isPaused) {
                Button lobbyButton = GameObject.Find("PauseLobbyButton").GetComponent<Button>();
            if (currentScene.name == "Lobby") {
                lobbyButton.interactable = false;
                lobbyButton.GetComponent<Image>().color = Color.grey;
            }
            else {
                lobbyButton.interactable = true;
                lobbyButton.GetComponent<Image>().color = Color.white;
            }
      }

    }
}