using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;

    void Start()
    {
        if (audioSource == null && AudioManager.Instance != null)
        {
            AudioSource[] sources = AudioManager.Instance.GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                audioSource = sources[0];
                Debug.Log("AudioSource assigned from AudioManager for effects on " + gameObject.name);
            }
            else
            {
                Debug.LogError("Not enough AudioSources on AudioManager for " + gameObject.name);
            }
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
            Debug.Log("Button sound played: " + soundClip.name);

            if (gameObject.name.Contains("Start") && SceneManager.GetActiveScene().name == "StartMenu")
            {
                // Optional: AudioManager.Instance.PlayBackgroundMusic();
            }
        }
        else
        {
            if (audioSource == null) Debug.LogError("AudioSource is null in ButtonSound on " + gameObject.name);
            if (soundClip == null) Debug.LogError("soundClip is null in ButtonSound on " + gameObject.name);
        }
    }
}