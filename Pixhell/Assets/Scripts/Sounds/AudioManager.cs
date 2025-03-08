using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music Tracks")]
    [SerializeField] private AudioClip lobbyTrack;      // For StartMenu, SelectRun, CharacterSelect, Limbo
    [SerializeField] private AudioClip gameplayTrack;  // Default for other scenes (e.g., Lust)
    private AudioSource gameplayBgmSource;             // For gameplay BGM
    private AudioSource lobbyBgmSource;                // For lobby BGM
    private AudioSource effectSource;
    private bool isGameplayMusicPlaying = false;
    private bool isLobbyMusicPlaying = false;
    private AudioClip currentGameplayTrack;
    private AudioClip currentLobbyTrack;

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
            lobbyBgmSource.volume = 0.015f; // Lobby default
            lobbyBgmSource.playOnAwake = false;

            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Initial scene: " + sceneName);
            currentGameplayTrack = null;
            currentLobbyTrack = null;
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
        AudioClip newTrack = GetTrackForScene(sceneName);
        Debug.Log("Track for " + sceneName + ": " + (newTrack != null ? newTrack.name : "null"));
        if (newTrack == null) return;

        // Stop both to ensure clean switch
        StopGameplayMusic();
        StopLobbyMusic();

        if (newTrack == lobbyTrack)
        {
            lobbyBgmSource.clip = newTrack;
            currentLobbyTrack = newTrack;
            currentGameplayTrack = null;
            PlayLobbyMusic();
            Debug.Log("Lobby BGM switched to: " + newTrack.name + " at volume " + lobbyBgmSource.volume);
        }
        else // Default to gameplayTrack
        {
            gameplayBgmSource.clip = newTrack;
            currentGameplayTrack = newTrack;
            currentLobbyTrack = null;
            PlayGameplayMusic();
            Debug.Log("Gameplay BGM switched to: " + newTrack.name + " at volume " + gameplayBgmSource.volume);
        }
    }

    private AudioClip GetTrackForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "StartMenu":
            case "SelectRun":
            case "CharacterSelect":
            case "Limbo":
                return lobbyTrack;
            default:
                return gameplayTrack;
        }
    }

    public void PlayGameplayMusic()
    {
        if (gameplayBgmSource != null && gameplayBgmSource.clip != null && !isGameplayMusicPlaying)
        {
            gameplayBgmSource.Play();
            isGameplayMusicPlaying = true;
            Debug.Log("Gameplay music started: " + gameplayBgmSource.clip.name + " at volume " + gameplayBgmSource.volume);
        }
        else
        {
            Debug.LogWarning("Gameplay music not started. Source: " + (gameplayBgmSource != null) + ", Clip: " + (gameplayBgmSource?.clip?.name ?? "null") + ", Playing: " + isGameplayMusicPlaying);
        }
    }

    public void PlayLobbyMusic()
    {
        if (lobbyBgmSource != null && lobbyBgmSource.clip != null && !isLobbyMusicPlaying)
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
            gameplayBgmSource.volume = Mathf.Clamp01(volume); // 0-0.5 range
            Debug.Log("Gameplay BGM volume set to: " + gameplayBgmSource.volume);
        }
    }

    public void SetLobbyMusicVolume(float volume)
    {
        if (lobbyBgmSource != null)
        {
            lobbyBgmSource.volume = Mathf.Clamp01(volume); // 0-0.5 range
            Debug.Log("Lobby BGM volume set to: " + lobbyBgmSource.volume);
        }
    }

    public void PlaySoundEffect(AudioClip clip, float volume = 1f)
    {
        if (effectSource != null && clip != null)
        {
            effectSource.PlayOneShot(clip, volume);
        }
    }

    public void SetEffectVolume(float volume)
    {
        if (effectSource != null)
        {
            effectSource.volume = Mathf.Clamp01(volume);
            Debug.Log("Effect volume set to: " + effectSource.volume);
        }
    }
}