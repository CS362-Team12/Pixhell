using UnityEngine;

public class ArcherClass : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectilePrefab;
    void Start()
    {
        base.Start();
        damage = 15.0f;
        attack_speed = 1.5f;
        speed_mult = 1.2f;
    }

    // Update is called once per frame



    protected override void BasicAttack()
    {
        if (!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        {
            if (Time.time - attack_time >= attack_speed * attack_speed_mult)
            {
                attack_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;
                GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(direction, 6.5f, damage, damage_mult);
            }
        }
    }
}
