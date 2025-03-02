using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{   
    public GameObject gameOverUI;
    bool isGameOver = false;
    private string prevScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverUI.SetActive(false);
        prevScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (prevScene != currentScene)
        {
            TurnOffMenu();
        }
        prevScene = currentScene;
    }

    public void TurnOnMenu() {
        if (!isGameOver) {
            isGameOver = true;
            gameOverUI.SetActive(true); 
            GameObject pauseManager = GameObject.Find("EventSystem");
            pauseManager.GetComponent<PauseController>().enabled = false;
        }
    }

    public void TurnOffMenu() {
        if (isGameOver) {
            isGameOver = false;
            gameOverUI.SetActive(false);
            GameObject pauseManager = GameObject.Find("EventSystem");
            pauseManager.GetComponent<PauseController>().enabled = true;
        }
    }
}
