using UnityEngine;

public class XPOrbSound : MonoBehaviour
{
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();
        }
    }

    void PlayPickupSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySoundEffect(pickupSound, 0.15f);
            Debug.Log("XP Orb sound played: " + pickupSound.name + " at volume 1.0");
        }
    }
}