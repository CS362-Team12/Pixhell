using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound; // Assign in Inspector

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioManager.Instance.PlaySoundEffect(clickSound, 0.2f);
            Debug.Log("Button sound played: " + clickSound.name);
        }

        else
        {
            Debug.LogWarning("No click sound assigned to ButtonSound on " + gameObject.name);
        }
    }
}