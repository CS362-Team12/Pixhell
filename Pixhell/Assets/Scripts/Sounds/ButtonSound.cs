using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            audioSource = AudioManager.Instance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                Debug.Log("Linked to AudioManager.Instance’s AudioSource on " + gameObject.name);
            }
            else
            {
                Debug.LogError("AudioManager.Instance has no AudioSource!");
            }
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null on " + gameObject.name);
            audioSource = gameObject.AddComponent<AudioSource>(); // Fallback
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
            Debug.Log("Played sound: " + soundClip.name);
        }
        else
        {
            Debug.LogError("Cannot play sound on " + gameObject.name + ": AudioSource or SoundClip is null");
        }
    }
}