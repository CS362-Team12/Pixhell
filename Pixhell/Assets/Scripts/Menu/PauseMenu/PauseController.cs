using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused;
    private string prevScene;
    public Slider bgmSlider;

    void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        else
        {
            Debug.LogError("pauseMenuUI is not assigned in PauseController");
        }
        prevScene = SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "StartMenu" || SceneManager.GetActiveScene().name == "SelectRun")
        {
            AudioManager.Instance.StopBackgroundMusic();
        }

        // Setup slider
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            bgmSlider.minValue = 0f;
            bgmSlider.maxValue = 0.5f;
            bgmSlider.value = 0.1f;
            Debug.Log("BGM Slider initialized");
        }
        else
        {
            Debug.LogWarning("bgmSlider is not assigned in PauseController");
        }
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        bool itemShopClosed = GameObject.Find("ItemShop") != null ? !GameObject.Find("ItemShop").GetComponent<ItemShop>().shopShowing : true;
        if (prevScene != currentScene && isPaused && itemShopClosed)
        {
            TogglePause(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && itemShopClosed)
        {
            TogglePause();
        }
        prevScene = currentScene;
    }

    public void TogglePause(bool allowToggle = false)
    {
        if ((SceneManager.GetActiveScene().name == "StartMenu" || SceneManager.GetActiveScene().name == "SelectRun") && !allowToggle)
        {
            return;
        }
        GameManager.LogPlayerData();

        isPaused = !isPaused;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(isPaused);
        }
        Time.timeScale = isPaused ? 0f : 1f;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = !isPaused;
            SceneButtonBehavior();

            if (isPaused)
            {
                AudioManager.Instance.PauseBackgroundMusic();
            }
            else
            {
                AudioManager.Instance.ResumeBackgroundMusic();
            }
        }
    }

    void SceneButtonBehavior()
    {
        if (isPaused)
        {
            Button lobbyButton = GameObject.Find("PauseLobbyButton").GetComponent<Button>();
            if (SceneManager.GetActiveScene().name == "Limbo")
            {
                lobbyButton.interactable = false;
                lobbyButton.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                lobbyButton.interactable = true;
                lobbyButton.GetComponent<Image>().color = Color.white;
            }
        }
    }

    void SetBGMVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }
}