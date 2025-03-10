using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;


public class LargeQuiver_U_A : Upgrade
{
    float arrowIncrease;
    
    public LargeQuiver_U_A(string t, string d, int r, float ai) {
        title = t;
        description = d;
        rarity = r;
        arrowIncrease = ai;
        reusable = false;
        validCharacters = new string[] {"Archer"};
    }

    public override void ApplyUpgrade()
    {   
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<ArcherClass>().IncreaseArrowVolley(arrowIncrease);
    }
}
