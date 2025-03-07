using System.Collections;
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
        timers = new float[] { 4f, 0.75f, 0f, 0.75f };
        coinLevel = 1;
    }

    public override void Start() {
        base.Start();
        Vector3 relativePos = transform.position - player.transform.position;
        angle = Mathf.Atan2(relativePos.x, relativePos.y);

        orbitRadius *= Random.Range(0.8f, 1.2f);
        orbitSpeed *= Random.Range(0.8f, 1.2f);

        if (Random.Range(0, 1) == 0) {
            orbitSpeed *= -1;
        }
    }

    public override void Move() {
        // var player = GameObject.FindWithTag("Player");
        animator.SetBool("is_moving", true);
        animator.SetBool("is_shooting", false);
        angle += orbitSpeed * Time.deltaTime;

        // Calculate the new position using trig
        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;
        
        // Update the position of the object (relative to the target's position) 
        Vector3 target = new Vector3(player.transform.position.x + x, player.transform.position.y + y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    private IEnumerator AttackCoroutine()
    {
        animator.SetTrigger("attack");

        // Wait for the animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length -.5f);

        // Instantiate projectile after animation finishes
        Vector2 direction = ((Vector2)(player.transform.position - transform.position)).normalized;
        Vector2 spawnPosition = transform.position + new Vector3(0, -.07f);
        GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
        projectile.Launch(direction, 6.5f);
    }

    public override void Attack() {
        StartCoroutine(AttackCoroutine());
    }

}
