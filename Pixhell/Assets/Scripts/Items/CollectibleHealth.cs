using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    float health =25.0f;
    public Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioClip pickupSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("hp_rotate"); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null && controller.health < controller.max_health)
        {
            controller.ChangeHealth(health);
            if (pickupSound != null)
            {
                AudioManager.Instance.PlaySoundEffect(pickupSound, 0.35f);
                Debug.Log("Health pickup sound played: " + pickupSound.name);
            }
            else
            {
                Debug.LogWarning("No pickup sound assigned to CollectibleHealth");
            }
            Destroy(gameObject);
        }

    }
}
