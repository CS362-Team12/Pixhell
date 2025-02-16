using UnityEngine;

public class Enemy : MonoBehaviour
{    
    protected float health = 100.0f;
    protected float speed = 1.0f;
    protected float collisionDamage = 25.0f;

    public GameObject player;

    // Three states, hopefully turned into constants later:
    // 0. Moving: Perform the move code
    // 1. Attack: Perform the attack animation and effects
    // 2. Idle: Used to basically just do nothing, can be used after attacking for a delay

    // These should be overwritten for most enemies

    // This array says which state it is in for certain times
    protected int[] states = { 0, 1, 2 };
    
    // Tracks how long each state lasts. 0 means just for one frame
    protected float[] timers = { 2f, 0f, 1f };
    
    // IMPORTANT: This tracks the INDEX of states currently being used. 
    // states[currIndex] = currState; There is a function for this


    int currIndex = 0;
    //How long the enemy has been in its current state
    float currStateTime = 0;

    int GetCurrentState()
    {
        return states[currIndex];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var currState = GetCurrentState();
        if (currState == 0) {
            Move();
        } else if (currState == 1) {
            Attack();
        } else if (currState == 2) {
            // If you wish to do something in the Idle phase
            Idle();
        }
        
        //Update timer
        currStateTime += Time.deltaTime;
        //If time is passed, move to next state, wrapping
        if (currStateTime >= timers[currIndex]) {
            currIndex = (currIndex+1) % states.Length;
            currStateTime = 0;
        }
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
        Debug.Log("Attack not implemented yet");
    }

    // Default, nothing 
    public virtual void Idle() 
    {
        return;
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
        Debug.Log("Took " + damage + " damage!");
        if (health <= 0) {
            //Should be replaced with a death animation? 
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collision Damage
        var target = other.GetComponent<PlayerController>(); 
        if (target != null)
        {
            target.TakeDamage(collisionDamage);  // Call the TakeDamage method
        }
    }
}
