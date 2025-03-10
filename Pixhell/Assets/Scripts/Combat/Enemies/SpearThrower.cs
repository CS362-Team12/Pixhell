using System.Collections;
using UnityEngine;
using static GameConstants;

public class SpearThrower : Enemy
{   

    public GameObject projectilePrefab; 
    
    float angle = 0f;   // Angle in radians, used for calculating the orbit

    public SpearThrower()
    {
        // Move, then wait a little, launch arrow, wait a little
        states = new int[] { MOVING, IDLING, ATTACKING, IDLING, ATTACKING, IDLING };
        timers = new float[] { 3f, 1f, 0f, 1f, 0f, 0.5f };
        coinLevel = 2;
    }

    public override void Start() {
        base.Start();
        Vector3 relativePos = transform.position - player.transform.position;
        angle = Mathf.Atan2(relativePos.x, relativePos.y);
        speed = 1.1f;
        max_health *= 1.75;
        health = max_health;
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
        projectile.Launch(direction, 9f);
    }

    public override void Attack() {

        StartCoroutine(AttackCoroutine());
    }

}
