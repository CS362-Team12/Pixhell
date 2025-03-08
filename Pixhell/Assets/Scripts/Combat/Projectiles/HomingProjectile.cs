using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingProjectile : EnemyProjectile
{
    GameObject player;
    float rotationSpeed = 75f; // Degrees per second 

    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector2 optimalDirection = ((Vector2)(player.transform.position - transform.position)).normalized;
        float targetAngle = Mathf.Atan2(optimalDirection.y, optimalDirection.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        Debug.Log("Angles " + angleDifference);
        float rotationStep = Mathf.Clamp(angleDifference, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);
        Debug.Log("Set " + rotationStep);
        transform.rotation = Quaternion.Euler(0, 0, currentAngle + rotationStep);
        rigidbody2d.linearVelocity = transform.right * rigidbody2d.linearVelocity.magnitude;

        
    }
}