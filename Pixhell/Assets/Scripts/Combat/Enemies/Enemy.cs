using UnityEngine;

using static GameConstants;
using System.Collections;
using TMPro;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float max_health = 100.0f;
    public float health = 100.0f;
    protected float speed = 1.0f;
    protected float collisionDamage = 25.0f;
    public bool facingRight = true;
    private bool is_dead = false;
    protected float chargeDistance = 10f;
    protected float chargeTime = 0.8f;

    public bool is_boss = false;
    public string boss_name;
    private BossBar boss_script;

    public int coinLevel = 1;

    public Animator animator;

    protected GameObject player;
    public GameObject XPDrop;
    public GameObject DamageText;
    [SerializeField] FloatingHpBar healthBar;
    public GameObject player_hp;

    [Header("Audio Settings")]
    [SerializeField] protected AudioClip deathSound;

    // Three states, hopefully turned into constants later:
    // 1. Moving: Perform the move code
    // 2. Attack: Perform the attack animation and effects
    // 3. Idle: Used to basically just do nothing, can be used after attacking for a delay

    // These should be overwritten for most enemies

    // This array says which state it is in for certain times
    protected int[] states = { MOVING, ATTACKING, IDLING, CHARGING };
    
    // Tracks how long each state lasts. 0 means just for one frame
    protected float[] timers = { 2f, 0f, 1f, 2f };
    
    // IMPORTANT: This tracks the INDEX of states currently being used. 
    // states[currIndex] = currState; There is a function for this


    int currIndex = 0;
    //How long the enemy has been in its current state
    float currStateTime = 0;
    // To add randomness, we store the current timer so we can add randomness to it
    float currTimer;

    private Vector3 chargeStartPosition;
    private Vector3 chargeTargetPosition;
    private Vector3 chargeDirection;
    [SerializeField] private float distanceCharged;

    int GetCurrentState()
    {
        return states[currIndex];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        health = max_health;
        if (!is_boss)
        {
            healthBar = GetComponentInChildren<FloatingHpBar>();
            healthBar.UpdateHealthBar(health, max_health);
        }

        
        player = GameObject.FindWithTag("Player");
        // Multiply by a scale, so that it's relative
        currTimer = timers[currIndex] * Random.Range(0.8f, 1.2f);
        if (is_boss)
        {
            GameObject bar_canvas = GameObject.Find("BossBar");


            Transform bar_slider_transform = bar_canvas.transform.Find("Boss");
            GameObject bar_slider = bar_slider_transform.gameObject;
            bar_slider.SetActive(true);

            boss_script = bar_slider.GetComponent<BossBar>();

            Transform bar_text_trans = bar_slider.transform.Find("BossName");
            GameObject bar_text = bar_text_trans.gameObject;

            TextMeshProUGUI boss_text = bar_text.GetComponent<TextMeshProUGUI>();

            boss_text.text = boss_name;
            boss_script.update_boss(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_dead)
        {
            var currState = GetCurrentState();
            if (currState == MOVING)
            {
                Move();
            }
            else if (currState == ATTACKING)
            {
                Attack();
            }
            else if (currState == IDLING)
            {
                // If you wish to do something in the Idle phase
                Idle();
            }
            else if (currState == CHARGING)
            {
                if (distanceCharged == -1f)
                {
                    chargeStartPosition = transform.position;
                    chargeTargetPosition = player.transform.position;
                    chargeDirection = (chargeTargetPosition - chargeStartPosition).normalized;
                    distanceCharged = 0f;
                    animator.SetTrigger("dash");
                }
                Charge();
            }
            else if (currState == HOMINGATTACK)
            {
                HomingShot();
            }

            float x = gameObject.transform.position.x;
            float player_x = player.transform.position.x;
            if (((x > player_x && facingRight) || (x < player_x && !facingRight)))
            {
                Flip();
            }
            //Update timer, with randomness so each enemy is a little different. 
            // Spawns should also spawn in increments if possible
            currStateTime += Time.deltaTime;
            //If time is passed, move to next state, wrapping
            if (currStateTime >= currTimer)
            {
                currIndex = (currIndex + 1) % states.Length;
                currStateTime = 0;
                currTimer = timers[currIndex] * Random.Range(0.8f, 1.2f);
                ResetVariables();
            }
        }
    }

    void ResetVariables()
    {
        distanceCharged = -1;
    }

    // Default, move towards player
    public virtual void Move() 
    {
        var step = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    // Default, nothing as of right now
    public virtual void Attack() 
    {
        // Debug.Log("Attack not implemented yet");
    }

    // Default, nothing 
    public virtual void Idle() 
    {
        return;
    }

    public virtual void Charge()
    {
        if (distanceCharged < chargeDistance)
        {
            
            transform.position += chargeDirection * (chargeDistance / chargeTime) * Time.deltaTime;
            distanceCharged += (chargeDistance / chargeTime) * Time.deltaTime;
        }
    }

    public virtual void HomingShot() {
        
    }

    public void TakeDamage(float damage) 
    {
        if (!is_dead) {
            health -= damage;

            if (is_boss)
            {
                boss_script.UpdateHealthBar(health, max_health);
            } else
            {
                healthBar.UpdateHealthBar(health, max_health);
            }

            
            Debug.Log("Took " + damage + " damage!");
            animator.SetTrigger("hit");
            if (health <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                animator.SetTrigger("dead");
                animator.SetBool("is_dead", true);
                is_dead = true;
                StartCoroutine(Die());
                
            }

            // Trigger floating text to show damage
            ShowDamageText(damage);

        }

    }

    void ShowDamageText(float damage)
    {
        GameObject DmgText = Instantiate(DamageText, transform.position, Quaternion.identity, transform);
        if (is_boss)
        {
            DmgText.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        }
        // DmgText.transform.SetParent(transform.parent, true);
        DmgText.transform.position = transform.position;
        
        if (!facingRight)
        {
            Debug.Log("FLIPS BEFORE: " + DmgText.transform.localScale.x);
            DmgText.transform.localScale = new Vector3(-1 * DmgText.transform.localScale.x, DmgText.transform.localScale.y, DmgText.transform.localScale.z);
            Debug.Log("FLIPS: " + DmgText.transform.localScale.x);
        }
        
        DmgText.GetComponent<TextMesh>().text = damage.ToString();
        Debug.Log("FLIPS AFTER: " + DmgText.transform.localScale.x);
    }

    private IEnumerator Die()
    {
        // Get the length of the teleport animation
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("Enemy Slain");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySoundEffect(deathSound, 0.2f);
            Debug.Log("Enemy death sound played: " + (deathSound != null ? deathSound.name : "none"));
        }

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationDuration);

        Instantiate(XPDrop, transform.position, Quaternion.identity);
        GameManager.coins += CoinCalculator();
        int random_num = Random.Range(0, 9);
        if (DEBUG) {
            random_num = Random.Range(0, 2);
        }
        if (random_num == 0)
        {
            Instantiate(player_hp, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collision Damage
        var target = other.GetComponent<PlayerController>(); 
        if (target != null && !is_dead)
        {
            target.TakeDamage(collisionDamage);  // Call the TakeDamage method
        }
    }

    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        if (!is_boss)
        {
            Vector3 childscale = healthBar.transform.localScale;
            childscale.x *= -1;
            healthBar.transform.localScale = childscale;
        }
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected int CoinCalculator() {
        float randomValue = (UnityEngine.Random.value + 0.5f) * coinLevel * coinLevel;
        return (int) Mathf.Floor(randomValue);
    }
}
