using UnityEngine;
using static GameConstants;

public class Glob : Enemy
{   
    public Glob()
    {
        states = new int[] { MOVING, IDLING, CHARGING, IDLING };
        timers = new float[] { 3f, 1f, 1.75f, 1.5f };
        speed = 1f;
        max_health *= .8;
        collisionDamage = 20f;
        coinLevel = 1;
        chargeDistance = 10f;
    }

    public override void Start()
    {
        base.Start();
        GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(1, 255), (byte)Random.Range(1, 255), (byte)Random.Range(1, 255), 255);
    }
    
}
