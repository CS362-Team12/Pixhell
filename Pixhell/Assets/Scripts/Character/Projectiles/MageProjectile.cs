using UnityEngine;

public class MageProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float damage;
    public LayerMask enemy_hit;
    public float aoe_radius = 2f;

    // Awake is called when the Projectile GameObject is instantiated
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    public void Launch(Vector2 direction, float force, float force_mult, float dam, float dam_mult)
    {
        rigidbody2d.AddForce(direction.normalized * (force * force_mult), ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = dam * dam_mult;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<Enemy>();
        if (target != null)
        {
            target.TakeDamage(damage);  // Call the TakeDamage method

        }
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Vector2 explosion_point = transform.position;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(explosion_point, aoe_radius, enemy_hit);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy target = enemy.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage * .8f);
            }
        }

    }
}
