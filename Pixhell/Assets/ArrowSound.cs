using UnityEngine;

public class ArrowSound : MonoBehaviour
{
    public AudioClip arrowFireSound;
    [SerializeField] private bool isPlayerArrow = true;

    void Start()
    {
        if (!isPlayerArrow) return; // Skip sound for non-player arrows

        AudioManager.Instance.PlaySoundEffect(arrowFireSound, 0.15f);
    }
}