using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction MoveAction;
    public InputAction SprintAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    float base_speed = 1.0f;
    float speed_mult;
    public float speed = 3.0f;
    public void update_movement(float increase)
    {
        speed_mult += increase;
    }
    float attack_speed = 1.0f;
    float attack_speed_mult = 1.0f;
    float attack_time = Time.time;
    public void as_increase(float increase)
    {
        attack_speed_mult -= increase;
    }


    public GameObject projectilePrefab;

    public float max_health = 100;
    float current_health;
    public float health { get { return current_health; } }

    public InputAction projectileCreate;

    Vector2 moveDirection = new Vector2(1, 0);

    void Start()
    {
        speed_mult = base_speed;
        current_health = max_health;
        rigidbody2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        SprintAction.Enable();
        projectileCreate.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        if (SprintAction.IsPressed())
        {
            Vector2 position = (Vector2)transform.position + move * 8.0f * Time.deltaTime * speed_mult;
            transform.position = position;
        }
        else
        {
            Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime * speed_mult;
            transform.position = position;
        }
        if (projectileCreate.IsPressed()) {
            Launch();
        }
    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void change_health(float health)
    {
        current_health = Mathf.Clamp(current_health + health, 0, max_health);
        Debug.Log(current_health + "/" + max_health);
    }

    public void update_health(float increase) 
    {
        max_health += increase;
        current_health = max_health;
    }
    void Launch()
    {
        if (Time.time - attack_time >= attack_speed*attack_speed_mult)
        {
            attack_time = Time.time;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up *.25f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(direction, 4.5f);
        }
        
    }
}
