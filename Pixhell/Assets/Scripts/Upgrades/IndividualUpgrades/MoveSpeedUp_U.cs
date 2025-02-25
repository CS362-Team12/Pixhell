using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class MoveSpeedUp_U : Upgrade
{
    float moveSpeedUp;
    
    public MoveSpeedUp_U(string t, string d, int r, float msu) {
        title = t;
        description = d;
        rarity = r;
        moveSpeedUp = msu;
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().UpdateSpeed(moveSpeedUp);
    }
}
