using UnityEngine;
using static GameConstants;
using System.Collections;

public class Gluttony : Enemy
{   
    public GameObject homingProjectilePrefab; 
    public GameObject projectilePrefab;

    public int volleyCount = 6;

    public Gluttony()
    {
        // Move, then wait a little, launch arrow, wait a little
        states = new int[] { MOVING, IDLING, HOMINGATTACK, IDLING, HOMINGATTACK, IDLING, HOMINGATTACK, IDLING, MOVING, IDLING, ATTACKING, IDLING, ATTACKING, IDLING };
        timers = new float[] { 2f, 0.5f, 0f, 0.5f, 0f, 0.5f, 0f, 0.5f, 2f, 1f, 0f, 1f, 0f, 1f };

        states = new int[] { MOVING, ATTACKING };
        timers = new float[] { 2f, 0f };
        speed = 1.5f;
        max_health = 2500f;
        collisionDamage = 50f;
        coinLevel = 5;
        is_boss = true;
    }

    private IEnumerator AttackCoroutine()
    {
        // Shoots volleys of 
        animator.SetTrigger("attack");

        // Wait for the animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length -.5f);

        // Instantiate projectile after animation finishes

        for (float i = 0; i < volleyCount; i+= 1)
        {
            float x = Mathf.Cos((3 / 2 * Mathf.PI) + (((i + 0.5f) / (volleyCount)) * Mathf.PI));
            float y = Mathf.Sin((3 / 2 * Mathf.PI) + (((i + 0.5f) / (volleyCount)) * Mathf.PI));
            Debug.Log(i);
            Debug.Log(x);
            Debug.Log(y);
            Vector2 spawnPosition = transform.position + new Vector3(0, -.07f);
            Vector2 direction = new Vector2(y, x);
            GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Launch(direction, 6.5f);

            direction = new Vector2(-y, x);
            projectileObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Launch(direction, 6.5f);

            yield return new WaitForSeconds(0.25f / volleyCount);
        }
        
    }

    public override void Attack() {
        StartCoroutine(AttackCoroutine());
    }
    
}
