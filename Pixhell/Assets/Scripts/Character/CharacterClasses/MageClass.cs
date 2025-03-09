using UnityEngine;

public class MageClass : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectilePrefab;
    private float aoe_mult = 1.0f;
    protected override void Start()
    {
        base.Start();
        max_health *= .75f;
        current_health = max_health;
        damage *= .48f;
        speed_mult *= 1.2f;
        attack_speed_mult *= 1.15f;
        GameObject test = GameObject.FindWithTag("IconManager");
        test.GetComponent<IconManager>().InsertIcon("Mage");
    }

    public override void ResetPlayerStats()
    {
        Start();
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
                MageProjectile projectile = projectileObject.GetComponent<MageProjectile>();
                projectile.Launch(direction, 6.5f, projectile_speed_mult, damage, damage_mult, aoe_mult);
            }
        }
    }

    public void UpdateAreaOfEffect(float multIncrase) {
        aoe_mult += multIncrase;
    }
}
