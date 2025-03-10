using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused;
    private string prevScene;
    public Slider bgmSlider;
    public Slider lobbyBgmSlider;
    Spawner spawner;

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

        // Setup slider
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(SetGameplayBGMVolume);
            bgmSlider.minValue = 0f;
            bgmSlider.maxValue = 0.5f;
            bgmSlider.value = 0.1f; // Gameplay default
            Debug.Log("Gameplay BGM Slider initialized");
        }

        if (lobbyBgmSlider != null)
        {
            lobbyBgmSlider.onValueChanged.AddListener(SetLobbyBGMVolume);
            lobbyBgmSlider.minValue = 0f;
            lobbyBgmSlider.maxValue = 0.3f;
            lobbyBgmSlider.value = 0.08f; // Lobby default
            Debug.Log("Lobby BGM Slider initialized");
        }

        spawner = GameObject.Find("WaveController").GetComponent<Spawner>();
    }

    void SetGameplayBGMVolume(float volume)
    {
        AudioManager.Instance.SetGameplayMusicVolume(volume);
    }

    void SetLobbyBGMVolume(float volume)
    {
        AudioManager.Instance.SetLobbyMusicVolume(volume);
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        bool itemShopClosed = GameObject.Find("ItemShop") != null ? !GameObject.Find("ItemShop").GetComponent<ItemShop>().shopShowing : true;
        if (prevScene != currentScene && isPaused && itemShopClosed)
        {
            TogglePause(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && itemShopClosed && !spawner.IsArenaCompleted())
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
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            SceneButtonBehavior();
        }
    }

    void SceneButtonBehavior()
    {
        if (isPaused)
        {
            Button lobbyButton = GameObject.Find("PauseLobbyButton").GetComponent<Button>();
            if (SceneManager.GetActiveScene().name == "Limbo" || SceneManager.GetActiveScene().name == "CharacterSelect")
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
}