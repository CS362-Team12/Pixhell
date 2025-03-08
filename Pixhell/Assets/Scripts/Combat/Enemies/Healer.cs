using System.Collections;
using UnityEngine;
using static GameConstants;

public class Healer : Enemy
{   

    public GameObject healCircle; 

    public Healer()
    {
        states = new int[] { MOVING, IDLING, ATTACKING, IDLING, MOVING };
        timers = new float[] { 4f, 0.75f, 0f, 0.75f, 4f };
        coinLevel = 1;
    }

    public override void Start() {
        base.Start();
        speed *= .3f;
    }
    
    public override void Move() {
        // var player = GameObject.FindWithTag("Player");
        animator.SetBool("is_moving", true);
        animator.SetBool("is_shooting", false);
        
        var closestEnemy = FindClosestEnemy();
        if (closestEnemy) {
            Debug.Log(closestEnemy);
            var step = speed*Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, step);
        } else {
            var step = speed*Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        }
        
    }

    GameObject FindClosestEnemy() {
        // Get all GameObjects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        // Current position
        Vector3 currentPosition = transform.position;

        // Loop through all objects with the tag
        foreach (GameObject obj in objectsWithTag)
        {
            // Skip itself and other healers
            Debug.Log(obj.name);
            if (obj.name == "HealerObject(Clone)") {
                continue;
            }

            // Calculate distance
            float distance = Vector3.Distance(currentPosition, obj.transform.position);

            // Check if it's the closest
            if (distance < closestDistance) {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }

    private IEnumerator AttackCoroutine()
    {
        animator.SetTrigger("attack");

        // Wait for the animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length -.5f);

        // Instantiate heal circle after animation finishes
        Vector2 spawnPosition = transform.position;
        GameObject healObject = Instantiate(healCircle, spawnPosition, Quaternion.identity);
        HealCircle heal = healObject.GetComponent<HealCircle>();
        heal.StartHealing();
    }

    public override void Attack() {
        StartCoroutine(AttackCoroutine());
    }

}
