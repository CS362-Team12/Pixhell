using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction MoveAction;
    public InputAction SprintAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    public GameObject projectilePrefab;

    public int max_health = 100;
    int currentHealth;
    public int health { get { return currentHealth; } }

    public InputAction projectileCreate;

    Vector2 moveDirection = new Vector2(1, 0);

    void Start()
    {
        currentHealth = 25;
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
            Vector2 position = (Vector2)transform.position + move * 8.0f * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime;
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

    public void change_health(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, max_health);
        Debug.Log(currentHealth + "/" + max_health);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
    }
}
