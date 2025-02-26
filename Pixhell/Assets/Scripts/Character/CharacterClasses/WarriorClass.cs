using UnityEngine;

public class WarriorClass : PlayerController
{
    public Transform AttackPoint;
    public float attack_range = .85f;
    public LayerMask enemyLayers;

    protected override void Start()
    {
        base.Start();
        max_health *= 1.25f;
        current_health = max_health;
        attack_speed *= 1.2f;
        speed_mult *= 1f;

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
                Vector2 forward_direction = transform.right;
                float attack_angle = 180f;
                animator.SetTrigger("Attack");
                attack_time = Time.time;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attack_range, enemyLayers);
                
                foreach (Collider2D enemy in hitEnemies)
                {
                    Vector2 enemy_direction = (enemy.transform.position - transform.position).normalized;
                    float angle = Vector2.Angle(forward_direction, enemy_direction);
                    if (m_FacingRight && angle <= attack_angle / 2 || !m_FacingRight && angle*-1 <= attack_angle*-1 / 2) // Only affect enemies in front of the swing
                    {
                        Enemy target = enemy.GetComponent<Enemy>();
                        if (target != null)
                        {
                            target.TakeDamage(damage*damage_mult); // Reduced AOE damage
                        }
                    }
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attack_range);
    }
}
