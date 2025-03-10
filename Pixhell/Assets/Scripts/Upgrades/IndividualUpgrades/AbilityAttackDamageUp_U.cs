using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class AbilityAttackDamageUp_U : Upgrade
{
    float abilityAttackDamageUp;
    
    public AbilityAttackDamageUp_U(string t, string d, int r, float aadu) {
        title = t;
        description = d;
        rarity = r;
        abilityAttackDamageUp = aadu;

        // Only for the archer till other abilities are implemented
        // Then make sure the correct variable is used for the damage_mult
        validCharacters = new string[] {"Archer"};
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().UpdateAbilityDamage(abilityAttackDamageUp);
    }
}
