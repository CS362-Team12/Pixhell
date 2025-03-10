using UnityEngine;

public class SideWallCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public bool isRight = false; // Must be set manually

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
            if (isRight)
            {
                other.transform.position = new Vector3(other.transform.position.x - (0.15f * movementMultiplier), other.transform.position.y, other.transform.position.z);
            }
            else 
            {
                other.transform.position = new Vector3(other.transform.position.x + (0.15f * movementMultiplier), other.transform.position.y, other.transform.position.z);
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            float movementMultiplier = (other.CompareTag("Player")) ? 1f : 0.5f;
            if (isRight)
            {
                other.transform.position = new Vector3(other.transform.position.x - (0.4f * movementMultiplier), other.transform.position.y, other.transform.position.z);
            }
            else 
            {
                other.transform.position = new Vector3(other.transform.position.x + (0.4f * movementMultiplier), other.transform.position.y, other.transform.position.z);
            }
        }

    }

}