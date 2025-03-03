using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class MaxHealthUp_U : Upgrade
{
    float maxHealthUp;
    
    public MaxHealthUp_U(string t, string d, int r, float mhu) {
        title = t;
        description = d;
        rarity = r;
        maxHealthUp = mhu;
    }

    public override void ApplyUpgrade()
    {   
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        var increase = maxHealthUp*player.max_health;
        player.UpdateHealth(increase);
    }
}
