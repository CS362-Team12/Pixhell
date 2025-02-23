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
    protected Rigidbody2D rigidbody2d;
    protected Vector2 move;
    protected bool m_FacingRight = true;
    protected float horizontal_move;

    [Header("Move Speed")]
    protected float base_speed = 1.0f;
    protected float speed_mult;
    public float speed = 3.0f;

    [Header("Dash Settings")]
    protected float dodge_duration = .2f;
    protected float dodge_time = -2f;

    [Header("Attack Settings")]
    protected float attack_speed = 1.0f;
    protected float attack_speed_mult = 1.0f;
    protected float attack_time = -2f;

    [Header("Health Settings")]
    public float max_health = 100;
    protected float current_health;
    public float health { get { return current_health; } }
    protected float invincibility_time = 1f;
    public bool is_vulnerable = true;
    protected float hit_time = -2f;
    protected float seconds = 1f;

    [Header("Damage Settings")]
    protected float damage_mult = 1.0f;
    protected float damage = 25.0f;



    public Animator animator;

    public void Start()
    {
        speed_mult = base_speed;
        current_health = max_health;
        rigidbody2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        SprintAction.Enable();
        DodgeAction.Enable();
        animator = GetComponent<Animator>();
        StartImmune();
    }

    public void update_movement(float increase)
    {
        speed_mult += increase;
    }

    // Update is called once per frame
    protected void Update()
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
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            BasicAttack();
        }
        
    }

    private IEnumerator dodge_roll(Vector2 move)
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

    protected void StartImmune()
    {
        is_vulnerable = false;
        StartCoroutine(ImmuneTimer());
    }

    private IEnumerator ImmuneTimer()
    {
        yield return new WaitForSeconds(seconds);
        is_vulnerable = true;
    }

    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected void FixedUpdate()
    {
        {
            Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }
    }

    // Run this function when taking damage from a damage source
    public bool TakeDamage(float damage) {
        bool damaged = ChangeHealth(-damage);
        if (current_health <= 0) {
            // GAME OVER
        }
        if (damaged)
        {
            hit_time = Time.time;
            StartImmune();
        }
        return damaged;
    }

    // Run this function when you don't want to trigger invinciblity frames
    public bool ChangeHealth(float health)
    {
        if (is_vulnerable && health < 0 && Time.time - hit_time >= invincibility_time)
        {
            current_health = Mathf.Clamp(current_health + health, 0, max_health);
            Debug.Log(current_health + "/" + max_health);
            return true;
        }
        return false;
    }

    public void UpdateHealth(float increase) 
    {
        max_health += increase;
        current_health = max_health;
    }

    public void UpdateImmunity(float increase)
    {
        seconds += increase;
    }

    public void AsIncrease(float increase)
    {
        attack_speed_mult -= increase;
    }

    public void DamageUpdate(float increase)
    {
        damage_mult += increase;
    }

    protected virtual void BasicAttack()
    {

        Debug.Log("Player Attacked");
    }
}