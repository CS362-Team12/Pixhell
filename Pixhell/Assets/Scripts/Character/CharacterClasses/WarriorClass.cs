using UnityEngine;

public class WarriorClass : PlayerController
{
    void Start()
    {
        base.Start();
        current_health = max_health;
        attack_speed *= 1.2f;
        speed_mult *= .8f;

    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            animator.SetTrigger("Attack");
            BasicAttack(move);
        }
    }
    protected override void BasicAttack(Vector2 move)
    { 
        base.BasicAttack(move);
    }
}
