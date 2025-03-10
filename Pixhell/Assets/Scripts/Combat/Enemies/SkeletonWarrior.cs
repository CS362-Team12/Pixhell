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
        timers = new float[] { 5f, 0.5f };
        
    }

    public override void Start() {
        base.Start();
        Vector3 relativePos = transform.position - player.transform.position;
        angle = Mathf.Atan2(relativePos.x, relativePos.y);
        speed = 1.3f;
        max_health = 250;
        collisionDamage = 50;
        chargeDistance = 3f;
        coinLevel = 2;
    }

}
