using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;
    private AudioSource bgmSource;
    private AudioSource effectSource;
    private bool isMusicPlaying = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            effectSource = gameObject.AddComponent<AudioSource>();
            bgmSource = gameObject.AddComponent<AudioSource>();
            if (backgroundMusic != null)
            {
                bgmSource.clip = backgroundMusic;
                bgmSource.loop = true;
                bgmSource.volume = 0.1f;
                bgmSource.playOnAwake = false;
                Debug.Log("BGM setup complete: " + backgroundMusic.name);
            }
            else
            {
                Debug.LogError("Background Music clip is not assigned in AudioManager");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip clip, float volume = 0.4f)
    {
        if (effectSource != null && clip != null)
        {
            effectSource.PlayOneShot(clip, volume);
            Debug.Log("Sound effect played: " + clip.name + " at volume " + volume);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (bgmSource != null && bgmSource.clip != null && !isMusicPlaying)
        {
            bgmSource.Play();
            isMusicPlaying = true;
            Debug.Log("Background music started: " + bgmSource.clip.name);
        }
        else
        {
            Debug.LogError("Cannot play BGM: bgmSource or clip is null, or already playing");
        }
    }

    public void PauseBackgroundMusic()
    {
        if (bgmSource != null && isMusicPlaying)
        {
            bgmSource.Pause();
            isMusicPlaying = false;
            Debug.Log("Background music paused");
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (bgmSource != null && !isMusicPlaying && bgmSource.clip != null)
        {
            bgmSource.UnPause();
            isMusicPlaying = true;
            Debug.Log("Background music resumed");
        }
    }

    public void StopBackgroundMusic()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
            isMusicPlaying = false;
            Debug.Log("Background music stopped");
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = Mathf.Clamp01(volume);
            Debug.Log("BGM volume set to: " + volume);
        }
    }
}