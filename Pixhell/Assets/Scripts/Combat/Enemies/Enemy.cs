using UnityEngine;

public class Enemy : MonoBehaviour
{    
    public float health = 100.0f;
    public float speed = 1.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Default, move towards player
    public virtual void Move() {
        var player = GameObject.FindWithTag("Player");
        var step = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Took " + damage + " damage!");
        if (health <= 0) {
            //Should be replaced with a death animation? 
            Destroy(gameObject);
        }
    }
}
