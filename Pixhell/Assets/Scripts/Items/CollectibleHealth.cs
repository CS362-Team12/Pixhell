using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float health =25.0f;
    float heal_mult = 1.0f;

    public void update_healing(float increase)
    {
        heal_mult += increase;
    }

    void Start()
    {
        
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
            controller.ChangeHealth(health*heal_mult);
            Destroy(gameObject);
        }

    }
}
