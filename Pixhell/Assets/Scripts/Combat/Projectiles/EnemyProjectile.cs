using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public float damage = 25.0f;

    // Awake is called when the Projectile GameObject is instantiated
    public virtual void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    public virtual void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<PlayerController>(); 
        if (target != null)
        {
            bool damaged = target.TakeDamage(damage);  // Call the TakeDamage method
            // This prevents the projectile from being destroyed when dashing
            if (!target.is_dodging) {
                Destroy(gameObject);
            }
        } else {
            // Don't collide and destory itself from enemies
            var enemy = other.GetComponent<Enemy>();
            if (enemy == null) { 
                Destroy(gameObject);
            }
        }

        
    }
}
