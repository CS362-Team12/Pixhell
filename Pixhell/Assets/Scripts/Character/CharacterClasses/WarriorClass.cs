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
    protected override void BasicAttack()
    { 
        base.BasicAttack();
    }
}
