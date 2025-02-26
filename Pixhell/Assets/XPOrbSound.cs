using UnityEngine;

public class XPOrbSound : MonoBehaviour
{
    public AudioClip pickupSound; // Assign in Inspector

    // Called when the orb collides with something (like the player)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it’s the player (adjust tag as needed)
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();
            // Don’t destroy here—let the existing script handle that
        }
    }

    void PlayPickupSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
                Debug.Log("XP Orb sound played: " + pickupSound.name);
            }
            else
            {
                Debug.LogError("AudioSource or pickupSound is null on XPOrb");
            }
        }
        else
        {
            Debug.LogError("AudioManager.Instance not found for XPOrb sound");
        }
    }
}