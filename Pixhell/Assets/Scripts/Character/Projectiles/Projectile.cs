using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float damage;

    // Awake is called when the Projectile GameObject is instantiated
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    public void Launch(Vector2 direction, float force, float dam, float dam_mult)
    {
        rigidbody2d.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = dam * dam_mult;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collision with " + other.gameObject);
        var target = other.GetComponent<Enemy>(); 
        if (target != null)
        {
            target.TakeDamage(damage);  // Call the TakeDamage method
        }

        Destroy(gameObject);
    }


}
