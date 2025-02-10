using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float damage_mult = 1.0f;
    float damage = 25.0f;
    public void damage_update(float increase)
    {
        damage_mult += increase;
    }


    // Awake is called when the Projectile GameObject is instantiated
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        // Destroy(gameObject, 5f);
    }

    void Update()
    {

    }


    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collision with " + other.gameObject);
        // Enemyclass controller = other.GetComponent<GameObject>;
        // controller.reduce_hp(damage*damage_mult); uncomment when enemy class is set up aint no way in hell im doing playable charater and enemys
        Destroy(gameObject);
    }


}
