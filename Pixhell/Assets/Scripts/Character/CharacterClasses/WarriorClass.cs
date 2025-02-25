using UnityEngine;

public class WarriorClass : PlayerController
{
    public Transform AttackPoint;
    public float attack_range = .55f;
    public LayerMask enemyLayers;

    protected override void Start()
    {
        base.Start();
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
                animator.SetTrigger("Attack");
                attack_time = Time.time;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attack_range, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Enemy target = enemy.GetComponent<Enemy>();
                    if (target != null)
                    {
                        target.TakeDamage(damage*damage_mult);
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
