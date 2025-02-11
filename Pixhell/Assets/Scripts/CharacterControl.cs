using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Threading;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Input Actions")]
    public InputAction MoveAction;
    public InputAction SprintAction;
    public InputAction DodgeAction;

    [Header("2D settings")]
    Rigidbody2D rigidbody2d;
    Vector2 move;
    private bool m_FacingRight = true;
    float horizontal_move;

    [Header("Move Speed")]
    float base_speed = 1.0f;
    float speed_mult;
    public float speed = 3.0f;

    [Header("Dash Settings")]
    bool is_vulnerable = true;
    float dodge_duration = .2f;
    float dodge_time = -2f;

    [Header("Attack Settings")]
    float attack_speed = 1.0f;
    float attack_speed_mult = 1.0f;
    float attack_time = -2f;

    [Header("Health Settings")]
    public float max_health = 100;
    float current_health;
    public float health { get { return current_health; } }

    public Animator animator;

    public GameObject projectilePrefab;

    void Start()
    {
        speed_mult = base_speed;
        current_health = 25;
        rigidbody2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        SprintAction.Enable();
        DodgeAction.Enable();
        animator = GetComponent<Animator>();
    }

    public void update_movement(float increase)
    {
        speed_mult += increase;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_move = Input.GetAxisRaw("Horizontal");
        Vector2 move = MoveAction.ReadValue<Vector2>();
        if (horizontal_move > 0 && !m_FacingRight)
        {
            Flip();
        }else if(horizontal_move < 0 && m_FacingRight)
        {
            Flip();
        }
        if (SprintAction.IsPressed() && move != Vector2.zero)
        {
            animator.SetFloat("speed", 6);
            Vector2 position = (Vector2)transform.position + move * 5.5f * Time.deltaTime * speed_mult;
            transform.position = position;
        }
        else if (MoveAction.IsPressed()) {
            animator.SetFloat("speed", 4);
            Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime * speed_mult;
            transform.position = position;
        }else {
                animator.SetFloat("speed", 0);
        }
        if (DodgeAction.WasPressedThisFrame())
        {
            StartCoroutine(dodge_roll(move));
        }
        if (Input.GetMouseButtonDown(0)) {
            Launch();
        }
        
    }

    IEnumerator dodge_roll(Vector2 move)
    {
        if (move == Vector2.zero) // Prevent dodge if there's no movement input
            yield break;

        if (Time.time - dodge_time >= 2.0f)
        {
            animator.SetBool("is_dodging", true);
            dodge_time = Time.time;
            is_vulnerable = false;
            float startTime = Time.time;
            while (Time.time < startTime + dodge_duration)
            {
                Vector2 position = (Vector2)transform.position + move * 15.5f * Time.deltaTime * speed_mult;
                transform.position = position;
                yield return null;
            }
            animator.SetBool("is_dodging", false);
            is_vulnerable = true;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate()
    {
        {
            Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }
    }

    public void change_health(float health)
    {
        if (is_vulnerable && health < 0)
        {
            current_health = Mathf.Clamp(current_health + health, 0, max_health);
            Debug.Log(current_health + "/" + max_health);
        }
    }

    public void update_health(float increase) 
    {
        max_health += increase;
        current_health = max_health;
    }

    public void as_increase(float increase)
    {
        attack_speed_mult -= increase;
    }

    void Launch()
    {
        if (!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        {
            if (Time.time - attack_time >= attack_speed * attack_speed_mult)
            {
                attack_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;
                GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(direction, 6.5f);
            }
        }
        
    }
}
