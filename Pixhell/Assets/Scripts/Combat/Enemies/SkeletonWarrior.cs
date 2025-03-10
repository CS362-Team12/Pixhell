using System.Collections;
using UnityEngine;
using static GameConstants;

public class SkeletonWarrior : Enemy
{   

    float angle = 0f;
    public SkeletonWarrior()
    {
        // Move, then wait a little, launch arrow, wait a little
        states = new int[] { MOVING, CHARGING };
        timers = new float[] { 5f, 0.7f };
        
    }

    public override void Start() {
        base.Start();
        Vector3 relativePos = transform.position - player.transform.position;
        angle = Mathf.Atan2(relativePos.x, relativePos.y);
        speed = 1.5f;
        max_health = 250;
        health = 250;
        collisionDamage = 50;
        chargeDistance = 5f;
        coinLevel = 2;
    }

}
