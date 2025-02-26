using UnityEngine;

public class ArrowSound : MonoBehaviour
{
    public AudioClip arrowFireSound;
    [SerializeField] private bool isPlayerArrow = true; // Set in Inspector

    void Start()
    {
        if (!isPlayerArrow) return; // Skip sound for non-player arrows

        if (AudioManager.Instance != null)
        {
            AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
            if (audioSource != null && arrowFireSound != null)
            {
                audioSource.PlayOneShot(arrowFireSound);
            }
        }
    }
}