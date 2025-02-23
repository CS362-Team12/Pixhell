using UnityEngine;
using static GameConstants;

public class Archer : Enemy
{   

    public GameObject projectilePrefab; 
    public float orbitRadius;  // The distance between the two objects
    public float orbitSpeed;  // Speed in which its oribit position changes

    float angle = 0f;   // Angle in radians, used for calculating the orbit

     public Archer()
    {
        // Move, then wait a little, launch arrow, wait a little
        states = new int[] { MOVING, IDLING, ATTACKING, IDLING };
        timers = new float[] { 3f, 0.75f, 0f, 0.75f };
    }

    public override void Move() {
        // var player = GameObject.FindWithTag("Player");
        angle += orbitSpeed * Time.deltaTime;

        // Calculate the new position using trig
        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;

        // Update the position of the object (relative to the target's position)
        Vector3 target = new Vector3(player.transform.position.x + x, player.transform.position.y + y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public override void Attack() {
        Vector2 direction = ((Vector2)(player.transform.position - transform.position)).normalized;
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up * .15f, Quaternion.identity);
        EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
        projectile.Launch(direction, 6.5f);
    }
}
