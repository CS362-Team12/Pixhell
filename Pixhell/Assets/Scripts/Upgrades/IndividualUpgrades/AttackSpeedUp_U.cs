using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class AttackSpeedUp_U : Upgrade
{
    float attackSpeedUp;
    
    public AttackSpeedUp_U(string t, string d, int r, float au) {
        title = t;
        description = d;
        rarity = r;
        attackSpeedUp = au;
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().UpdateAttackSpeed(attackSpeedUp);
    }
}
