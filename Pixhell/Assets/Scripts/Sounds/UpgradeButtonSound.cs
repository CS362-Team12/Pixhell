using UnityEngine;

public class UpgradeButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip selectSound; // Sound for selecting an upgrade

    public void PlaySelectSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
            if (audioSource != null && selectSound != null)
            {
                audioSource.PlayOneShot(selectSound);
                Debug.Log("Select upgrade sound played: " + selectSound.name);
            }
            else
            {
                Debug.LogError("AudioSource or selectSound is null in UpgradeButtonSound");
            }
        }
    }
}