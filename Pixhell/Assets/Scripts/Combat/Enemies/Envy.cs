using UnityEngine;
using static GameConstants;

public class Envy : Enemy
{   

    public Envy()
    {
        // Move, then wait a little, launch arrow, wait a little
        states = new int[] { MOVING, CHARGING, IDLING };
        timers = new float[] { 3f, 1f, 1.5f };
        speed = 1.5f;
        max_health = 1000f;
        collisionDamage = 35f;
        coinLevel = 3;

    }

}
