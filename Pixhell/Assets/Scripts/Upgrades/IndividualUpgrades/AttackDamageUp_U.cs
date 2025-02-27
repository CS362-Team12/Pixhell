using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class AttackDamageUp_U : Upgrade
{
    float attackDamageUp;
    
    public AttackDamageUp_U(string t, string d, int r, float adu) {
        title = t;
        description = d;
        rarity = r;
        attackDamageUp = adu;
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().UpdateDamage(attackDamageUp);
    }
}
