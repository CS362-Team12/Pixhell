using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class AttackSpeedUp_U : Upgrade
{
    public override string title { get; set; } = "Attack Speed Up";
    public override string description { get; set; } = "Increase attack speed by 20%.";
    public override int rarity { get; set; } = COMMON;
    float attack_up { get; set; } = 0.1f;

    public override void ApplyUpgrade()
    {   
        Debug.Log("HELLO");
        player.update_attack_speed(1);
    }
}
