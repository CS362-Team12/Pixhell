using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Threading;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public bool is_teleporting = false;

    [Header("Move Speed")]
    protected float base_speed = 0.9f;
    protected float speed_mult;
    public float speed = 3.0f;
    protected float stopTime = 0f;
    protected float minStopDuration = 0.05f;

    [Header("Dash Settings")]
    protected float dodge_duration = .2f;
    protected float dodge_time = -2f;
    public bool is_dodging = false;
    protected float dash_mult = 1.0f;
    public bool on_cooldown = false;

    [Header("Attack Settings")]
    protected float base_attack_speed = 1f;
    protected float attack_speed = 1.0f;
    protected float attack_speed_mult = 1.0f;
    protected float attack_time = -2f;

    [Header("Health Settings")]
    
    public float base_health = 100f;
    public float max_health = 100f;
    protected float current_health;
    public float health { get { return current_health; } }
    protected float invincibility_time = 1f;
    public bool is_vulnerable = true;
    protected float hit_time = -2f;
    protected float seconds = 1f;
    float heal_mult = 1.0f;
    protected bool is_dead = false;
    public float deaths = 0;


    [Header("Damage Settings")]
    protected float base_damage_mult = 1.0f;
    protected float damage_mult = 1.0f;
    protected float damage = 25.0f;
    protected float projectile_speed_mult = 1.0f;

    [Header("Audio Settings")]
    [SerializeField] protected AudioClip dodgeSound;
    [SerializeField] protected AudioClip damageSound;
    //[SerializeField] protected AudioClip attackSound;
    [SerializeField] protected AudioClip playerdeathSound;

    [Header("GameObjects")] // Gameobjects for character use
    public Animator animator;
    public GameObject slash_prefab;

    // Image for dodge cooldown indicator
    public Image DodgeImage;

    protected virtual void Start()
    {
        Debug.Log("start");
        // Sets base values
        speed_mult = base_speed;
        // Enables Movement
        DodgeImage = GameObject.Find("DashOnCooldown").GetComponent<Image>();
        Debug.Log(GameManager.inventory.totalHealthMod);
        speed_mult = (base_speed + GameManager.inventory.totalMovementSpeedMod);
        damage_mult = (base_damage_mult + GameManager.inventory.totalDamageMod);
        max_health = base_health * (1 + GameManager.inventory.totalHealthMod);
        attack_speed_mult = base_attack_speed + GameManager.inventory.totalAttackSpeedMod;
        rigidbody2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        SprintAction.Enable();
        DodgeAction.Enable();
        SpecialOne.Enable();
        SpecialTwo.Enable();
        animator = GetComponent<Animator>();
        DodgeImage.fillAmount = 0f;
        StartImmune();
    }

    public virtual void ResetPlayerStats()
    {
        Start();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (!is_dead)
        {
            horizontal_move = Input.GetAxisRaw("Horizontal");
            Vector2 move = MoveAction.ReadValue<Vector2>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetFloat("AnimationSpeed", attack_speed_mult);
            if (mousePosition.x > transform.position.x && !m_FacingRight)
            {
                Flip();
            }
            else if (mousePosition.x < transform.position.x && m_FacingRight)
            {
                Flip();
            }
            if (SprintAction.IsPressed() && move != Vector2.zero && !animator.GetBool("is_teleporting"))
            {
                animator.SetFloat("speed", 6);
                Vector2 position = (Vector2)transform.position + move * 5f * Time.deltaTime * speed_mult;
                transform.position = position;
            }
            else if (MoveAction.IsPressed() && !animator.GetBool("is_teleporting"))
            {
                animator.SetFloat("speed", 4);
                Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime * speed_mult;
                transform.position = position;
            }
            else
            {
                animator.SetFloat("speed", 0);
            }
            if (DodgeAction.WasPressedThisFrame())
            {
                StartCoroutine(dodge_roll(move));
            }
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                BasicAttack(move);
            }
            // Timer for not being able to attack while sprinting
            if (move == Vector2.zero)
            {
                stopTime += Time.deltaTime; // Increase stop duration
            }
            else
            {
                stopTime = 0f; // Reset when moving
            }
            if (on_cooldown)
            {
                DodgeImage.fillAmount = (2f - Time.time + dodge_time) / 2f;
                if (DodgeImage.fillAmount == 0f)
                {
                    on_cooldown = false;
                }
            }
        }

    }

    // Dodge function
    private IEnumerator dodge_roll(Vector2 move)
    {
        if (move == Vector2.zero) // Prevent dodge if there's no movement input
            yield break;

        if (Time.time - dodge_time >= 2.0f)
        {
            AudioManager.Instance.PlaySoundEffect(dodgeSound, 0.3f);
            animator.SetBool("is_dodging", true);
            is_dodging = true;
            dodge_time = Time.time;
            on_cooldown = true;
            is_vulnerable = false;
            float startTime = Time.time;
            while (Time.time < startTime + dodge_duration)
            {
                Vector2 position = (Vector2)transform.position + move * 30f * Time.deltaTime * dash_mult * (speed_mult/4f);
                transform.position = position;
                yield return null;
            }
            animator.SetBool("is_dodging", false);
            is_dodging = false;
            is_vulnerable = true;
        }
    }

    // StartImmune and ImmuneTimer are together. They determine the length of Invulnerability 
    public void StartImmune()
    {
        is_vulnerable = false;
        StartCoroutine(ImmuneTimer());
    }

    private IEnumerator ImmuneTimer()
    {
        float elapsedTime = 0f;
        float flashInterval = 0.1f; 
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Character is invulnerable");
        while (elapsedTime < seconds)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; 
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        // Ensure sprite is visible at the end
        spriteRenderer.enabled = true;
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
        if (current_health <= 0 && !is_dead) {
            deaths++;
            GameObject gameOverController = GameObject.Find("EventSystem");
            gameOverController.GetComponent<GameOverController>().TurnOnMenu();
            animator.SetTrigger("death");
            is_dead = true;
            AudioManager.Instance.PlaySoundEffect(playerdeathSound, 0.2f);
            Debug.Log("Player death sound played: " + (playerdeathSound != null ? playerdeathSound.name : "none"));
            StartCoroutine(FreezeOnDeath());
        }
        if (damaged && !is_dead)
        {
            hit_time = Time.time;
            StartImmune();
        }
        return damaged;
    }

    protected IEnumerator FreezeOnDeath()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DeathAnimation"))
        {
            yield break;
        }

        BoxCollider2D hitbox = GetComponent<BoxCollider2D>();
        hitbox.size = new Vector2(.6f, .25f);
        hitbox.offset = new Vector2(-.2f, -.2f);
        animator.SetTrigger("death");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length/2);

        animator.enabled = false;
    }

    // Run this function when you don't want to trigger invinciblity frames
    public bool ChangeHealth(float health)
    {
        if (is_vulnerable && health < 0 && Time.time - hit_time >= invincibility_time)
        {
            current_health = Mathf.Clamp(current_health + health, 0, max_health);
            Debug.Log(current_health + "/" + max_health);

            AudioManager.Instance.PlaySoundEffect(damageSound, 0.2f);
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
        current_health += increase;
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

    public bool IsDead() {
        return is_dead;
    }

    // Basic attacks for players
    protected virtual void BasicAttack(Vector2 move)
    {
        Debug.Log("Player Attacked");
        //AudioManager.Instance.PlaySoundEffect(attackSound, 0.2f);
        //Debug.Log("Player Attacked with sound: " + (attackSound != null ? attackSound.name : "none"));
    }

    protected virtual void Special1(Vector2 move)
    {
        Debug.Log("Player used ability 1");
    }

    protected virtual void Special2(int arrow_amount)
    {
        Debug.Log("Player used ability 2");
    }
}