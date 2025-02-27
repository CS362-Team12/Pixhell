using UnityEngine;

public class ArcherClass : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectilePrefab;
    protected override void Start()
    {
        base.Start();
        max_health *= .75f;
        current_health = max_health;
        damage *= .60f;
        speed_mult *= 1.2f;
        attack_speed_mult *= 1.1f;
    }

    protected override void Update()
    {
        base.Update();
      
    }
    protected override void BasicAttack(Vector2 move)
    {
        if ((!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        || (SprintAction.IsPressed() && stopTime >= minStopDuration && !DodgeAction.IsPressed()))
        {
            if (Time.time - attack_time >= attack_speed / attack_speed_mult)
            {
                animator.SetTrigger("Attack");
                attack_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;
                GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                ArcherProjectile projectile = projectileObject.GetComponent<ArcherProjectile>();
                projectile.Launch(direction, 6.5f, projectile_speed_mult, damage, damage_mult);
            }
        }
    }
}
