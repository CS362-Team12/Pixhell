using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class DashRangeUp_U : Upgrade
{
    float dashRangeUp;

    
    public DashRangeUp_U(string t, string d, int r, float dru) {
        title = t;
        description = d;
        rarity = r;
        reusable = false;
        dashRangeUp = dru;
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().UpdateDash(dashRangeUp);
    }
}