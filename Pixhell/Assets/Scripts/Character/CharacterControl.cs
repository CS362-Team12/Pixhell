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
    public InputAction SpecialOne;
    public InputAction SpecialTwo;
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
    public bool is_dodging = false;
    protected float dash_mult = 1.0f;

    [Header("Attack Settings")]
    protected float attack_speed = 1.0f;
    protected float attack_speed_mult = 1.0f;
    protected float attack_time = -2f;

    [Header("Health Settings")]
    public float max_health = 100f;
    protected float current_health;
    public float health { get { return current_health; } }
    protected float invincibility_time = 1f;
    public bool is_vulnerable = true;
    protected float hit_time = -2f;
    protected float seconds = 1f;
    float heal_mult = 1.0f;


    [Header("Damage Settings")]
    protected float damage_mult = 1.0f;
    protected float damage = 25.0f;
    protected float projectile_speed_mult = 1.0f;

    [Header("Audio Settings")] // New header for clarity
    [SerializeField] protected AudioClip dodgeSound;
    [SerializeField] protected AudioClip damageSound;

    public Animator animator;

    public void Start()
    {
        // Sets base values
        speed_mult = base_speed;
        // Enables Movement
        Debug.Log(GameManager.inventory.totalHealthMod);
        speed_mult = (1 + GameManager.inventory.totalMovementSpeedMod);
        damage_mult = (1 + GameManager.inventory.totalDamageMod);
        max_health = max_health * (1 + GameManager.inventory.totalHealthMod);
        current_health = max_health;
        attack_speed_mult = attack_speed_mult + GameManager.inventory.totalAttackSpeedMod;
        rigidbody2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        SprintAction.Enable();
        DodgeAction.Enable();
        animator = GetComponent<Animator>();
        StartImmune();
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
            if (AudioManager.Instance != null)
            {
                AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
                if (audioSource != null && dodgeSound != null)
                {
                    audioSource.PlayOneShot(dodgeSound);
                    Debug.Log("Dodge sound played: " + dodgeSound.name);
                }
                else
                {
                    Debug.LogError("AudioSource or dodgeSound is null in dodge_roll");
                }
            }

            animator.SetBool("is_dodging", true);
            is_dodging = true;
            dodge_time = Time.time;
            is_vulnerable = false;
            float startTime = Time.time;
            while (Time.time < startTime + dodge_duration)
            {
                Vector2 position = (Vector2)transform.position + move * 10f * Time.deltaTime * dash_mult * (speed_mult/4f);
                transform.position = position;
                yield return null;
            }
            animator.SetBool("is_dodging", false);
            is_dodging = false;
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

            // Play damage sound when health decreases
            if (AudioManager.Instance != null)
            {
                AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
                if (audioSource != null && damageSound != null)
                {
                    audioSource.PlayOneShot(damageSound);
                    Debug.Log("Damage sound played: " + damageSound.name);
                }
                else
                {
                    Debug.LogError("AudioSource or damageSound is null in ChangeHealth");
                }
            }

            return true;
        }
        else if (health > 0)
        {
            current_health = Mathf.Clamp(current_health + (health*heal_mult), 0, max_health);
            Debug.Log(current_health + "/" + max_health);
            return false;
        }
        return false;
    }
    // All increase modifiers
    public void UpdateProjectileSpeed(float increase)
    {
        projectile_speed_mult += increase;
    }

    public void UpdateSpeed(float increase)
    {
        speed_mult += increase;
    }

    public void UpdateDash(float increase)
    {
        dash_mult += increase;
    }

    public void UpdateHealing(float increase)
    {
        heal_mult += increase;
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

    public void UpdateAttackSpeed(float increase)
    {
        attack_speed_mult += increase;
    }

    public void UpdateDamage(float increase)
    {
        damage_mult += increase;
    }

    // Basic attacks for players
    protected virtual void BasicAttack()
    {
        Debug.Log("Player Attacked");
    }
}