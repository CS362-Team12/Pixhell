using UnityEngine;
using static GameConstants;
using System.Collections;

public class Gluttony : Enemy
{   
    public GameObject homingProjectilePrefab; 
    public GameObject fragmentingProjectilePrefab;

    public int volleyCount = 4;

    public Gluttony()
    {
        // Move, then wait a little, launch arrow, wait a little
        states = new int[] { MOVING, IDLING, ATTACKING, IDLING, HOMINGATTACK, IDLING };
        timers = new float[] { 3f, 1.5f, 0f, 1.5f, 0f, 0.5f};

        speed = 1.5f;
        max_health = 2000f;
        collisionDamage = 50f;
        coinLevel = 5;
        is_boss = true;
    }

    private IEnumerator AttackCoroutine()
    {
        // Shoots volleys of bullets
        animator.SetTrigger("attack");

        // Wait for the animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length -.5f);

        // Instantiate projectile after animation finishes

        for (float i = 0; i < volleyCount; i+= 1)
        {
            float x = Mathf.Cos((3 / 2 * Mathf.PI) + (((i + 0.5f) / (volleyCount)) * Mathf.PI));
            float y = Mathf.Sin((3 / 2 * Mathf.PI) + (((i + 0.5f) / (volleyCount)) * Mathf.PI));
            Vector2 spawnPosition = transform.position + new Vector3(0, -.07f);
            Vector2 direction = new Vector2(y, x);
            GameObject projectileObject = Instantiate(fragmentingProjectilePrefab, spawnPosition, Quaternion.identity);
            EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Launch(direction, 6.5f);

            direction = new Vector2(-y, x);
            projectileObject = Instantiate(fragmentingProjectilePrefab, spawnPosition, Quaternion.identity);
            projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Launch(direction, 6.5f);

            yield return new WaitForSeconds(0.25f / volleyCount);
        }
        
    }

    public override void Attack() {
        StartCoroutine(AttackCoroutine());
    }


    private IEnumerator HomingAttackCoroutine()
    {
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length -.5f);

        Vector2 spawnPosition = transform.position;
        Vector2 direction = ((Vector2)(player.transform.position - transform.position)).normalized;
        GameObject projectileObject = Instantiate(homingProjectilePrefab, spawnPosition, Quaternion.identity);
        EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
        projectile.Launch(direction, 6.5f);
    }

    public override void HomingShot()
    {
        StartCoroutine(HomingAttackCoroutine());
    }
}
