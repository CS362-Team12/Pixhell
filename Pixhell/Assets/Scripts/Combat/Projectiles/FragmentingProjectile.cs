using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FragmentingProjectile : EnemyProjectile
{
    public float timeToLive = 2f;
    public float currentTime = 0f;
    public bool willSplit = true;
    public float splitCount = 2;
    public GameObject projectilePrefab;

    override public void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToLive)
        {
            if (willSplit) {
                for (float i = 0; i < splitCount; i+= 1)
                {
                    float x = Mathf.Cos(i / splitCount * 2 * Mathf.PI);
                    float y = Mathf.Sin(i / splitCount * 2 * Mathf.PI);
                    Vector2 spawnPosition = transform.position;
                    Vector2 direction = new Vector2(x, y);
                    GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
                    EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
                    projectile.Launch(direction, 4f);
                }
            }
            Destroy(gameObject);
        }
    }
}