using UnityEngine;

public class MageClass : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        max_health = 75f;
        current_health = max_health;
        damage = 12.0f;
        speed_mult = 1.2f;
        attack_speed_mult = 1.15f;
    }
    protected override void BasicAttack()
    {
        base.BasicAttack();
    }
}
