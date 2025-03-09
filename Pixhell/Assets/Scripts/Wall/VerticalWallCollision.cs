using UnityEngine;

public class VerticalWallCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public bool isTop = false; // Must be set manually

    // Awake is called when the Projectile GameObject is instantiated
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            float movementMultiplier = (other.CompareTag("Player")) ? 1f : 0.5f;
            if (isTop)
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 0.15f, other.transform.position.z);
            }
            else 
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.15f, other.transform.position.z);
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            float movementMultiplier = (other.CompareTag("Player")) ? 1f : 0.5f;
            if (isTop)
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - (0.4f * movementMultiplier), other.transform.position.z);
            }
            else 
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + (0.4f * movementMultiplier), other.transform.position.z);
            }
        }

    }

}