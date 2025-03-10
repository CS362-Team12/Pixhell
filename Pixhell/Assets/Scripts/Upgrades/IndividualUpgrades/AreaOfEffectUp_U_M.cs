using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class AreaOfEffectUp_U_M : Upgrade
{
    float areaOfEffectUp;
    
    public AreaOfEffectUp_U_M(string t, string d, int r, float aoeu) {
        title = t;
        description = d;
        rarity = r;
        areaOfEffectUp = aoeu;
        reusable = false;
        validCharacters = new string[] {"Mage"};
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<MageClass>().UpdateAreaOfEffect(areaOfEffectUp);
    }
}
