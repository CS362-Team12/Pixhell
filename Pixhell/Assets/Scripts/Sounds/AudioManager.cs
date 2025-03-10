using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music Tracks")]
    [SerializeField] private AudioClip lobbyTrack;      // For StartMenu, SelectRun, CharacterSelect, Limbo
    [SerializeField] private AudioClip gameplayTrack;  // Default for other scenes (e.g., Lust, Gluttony)
    [SerializeField] private AudioClip bossTrack;      // For boss fights
    private AudioSource gameplayBgmSource;             // For gameplay BGM
    private AudioSource lobbyBgmSource;                // For lobby BGM
    private AudioSource effectSource;
    private bool isGameplayMusicPlaying = false;
    private bool isLobbyMusicPlaying = false;
    private AudioClip currentGameplayTrack;
    private AudioClip currentLobbyTrack;
    private bool isBossActive = false; // Tracks boss state

    [Header("Button Sound")]
    [SerializeField] private AudioClip buttonClickSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            effectSource = gameObject.AddComponent<AudioSource>();
            gameplayBgmSource = gameObject.AddComponent<AudioSource>();
            lobbyBgmSource = gameObject.AddComponent<AudioSource>();

            gameplayBgmSource.loop = true;
            gameplayBgmSource.volume = 0.1f; // Gameplay default
            gameplayBgmSource.playOnAwake = false;

            lobbyBgmSource.loop = true;
            lobbyBgmSource.volume = 0.08f; // Lobby default
            lobbyBgmSource.playOnAwake = false;

            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Initial scene: " + sceneName);      
            UpdateBGMForScene(sceneName);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        UpdateBGMForScene(scene.name);
    }

    private void UpdateBGMForScene(string sceneName)
    {
        StopGameplayMusic();
        StopLobbyMusic();

        switch (sceneName)
        {
            case "Limbo":
                lobbyBgmSource.clip = lobbyTrack;
                currentLobbyTrack = lobbyTrack;
                currentGameplayTrack = null;
                PlayLobbyMusic();
                Debug.Log("Lobby BGM started for Limbo: " + lobbyTrack.name + " at volume " + lobbyBgmSource.volume);
                break;
            case "Lust":
            case "Gluttony": // Add more gameplay scenes here
                gameplayBgmSource.clip = gameplayTrack;
                currentGameplayTrack = gameplayTrack;
                currentLobbyTrack = null;
                PlayGameplayMusic();
                Debug.Log("Gameplay BGM started for " + sceneName + ": " + gameplayTrack.name + " at volume " + gameplayBgmSource.volume);
                break;
            default:
                currentLobbyTrack = null;
                currentGameplayTrack = null;
                Debug.Log("No BGM for scene: " + sceneName);
                break;
        }
    }

    public void StartBossMusic()
    {
        if (gameplayBgmSource.clip != bossTrack)
        {
            StopGameplayMusic();
            gameplayBgmSource.clip = bossTrack;
            currentGameplayTrack = bossTrack;
            PlayGameplayMusic();
            isBossActive = true;
            Debug.Log("Boss music started: " + bossTrack.name);
        }
    }

    public void StopBossMusic()
    {
        if (isBossActive)
        {
            StopGameplayMusic();
            gameplayBgmSource.clip = gameplayTrack;
            currentGameplayTrack = gameplayTrack;
            PlayGameplayMusic();
            isBossActive = false;
            Debug.Log("Boss music stopped, reverted to: " + gameplayTrack.name);
        }
    }

    public void PlayGameplayMusic()
    {
        if (gameplayBgmSource != null && gameplayBgmSource.clip != null && !gameplayBgmSource.isPlaying)
        {
            gameplayBgmSource.Play();
            isGameplayMusicPlaying = true;
            Debug.Log("Gameplay music started: " + gameplayBgmSource.clip.name + " at volume " + gameplayBgmSource.volume);
        }
    }

    public void PlayLobbyMusic()
    {
        if (lobbyBgmSource != null && lobbyBgmSource.clip != null && !lobbyBgmSource.isPlaying)
        {
            lobbyBgmSource.Play();
            isLobbyMusicPlaying = true;
            Debug.Log("Lobby music started: " + lobbyBgmSource.clip.name + " at volume " + lobbyBgmSource.volume);
        }
    }

    public void PauseBackgroundMusic()
    {
        if (isGameplayMusicPlaying)
        {
            gameplayBgmSource.Pause();
            isGameplayMusicPlaying = false;
            Debug.Log("Gameplay music paused");
        }
        if (isLobbyMusicPlaying)
        {
            lobbyBgmSource.Pause();
            isLobbyMusicPlaying = false;
            Debug.Log("Lobby music paused");
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (!isGameplayMusicPlaying && gameplayBgmSource.clip != null)
        {
            gameplayBgmSource.UnPause();
            isGameplayMusicPlaying = true;
            Debug.Log("Gameplay music resumed");
        }
        if (!isLobbyMusicPlaying && lobbyBgmSource.clip != null)
        {
            lobbyBgmSource.UnPause();
            isLobbyMusicPlaying = true;
            Debug.Log("Lobby music resumed");
        }
    }

    public void StopGameplayMusic()
    {
        if (gameplayBgmSource != null)
        {
            gameplayBgmSource.Stop();
            isGameplayMusicPlaying = false;
            Debug.Log("Gameplay music stopped");
        }
    }

    public void StopLobbyMusic()
    {
        if (lobbyBgmSource != null)
        {
            lobbyBgmSource.Stop();
            isLobbyMusicPlaying = false;
            Debug.Log("Lobby music stopped");
        }
    }

    public void SetGameplayMusicVolume(float volume)
    {
        if (gameplayBgmSource != null)
        {
            gameplayBgmSource.volume = Mathf.Clamp01(volume);
            Debug.Log("Gameplay BGM volume set to: " + gameplayBgmSource.volume);
        }
    }

    public void SetLobbyMusicVolume(float volume)
    {
        if (lobbyBgmSource != null)
        {
            lobbyBgmSource.volume = Mathf.Clamp01(volume);
            Debug.Log("Lobby BGM volume set to: " + lobbyBgmSource.volume);
        }
    }

    public void PlaySoundEffect(AudioClip clip, float volume = 1f)
    {
        if (effectSource != null && clip != null)
        {
            effectSource.PlayOneShot(clip, volume);
            //test
        }
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            PlaySoundEffect(buttonClickSound, 0.5f);
            Debug.Log("Button click sound triggered: " + buttonClickSound.name); // Your log
        }
        else
        {
            Debug.LogWarning("No button click sound assigned in AudioManager");
        }
    }

    public void SetEffectVolume(float volume)
    {
        if (effectSource != null)
        {
            effectSource.volume = Mathf.Clamp01(volume);
            //Debug.Log("Effect volume set to: " + effectSource.volume);
        }
    }
}