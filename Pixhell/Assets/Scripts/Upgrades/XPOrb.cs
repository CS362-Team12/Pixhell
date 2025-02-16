using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int XPAmount = 1;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            // Get the position of the current object and the target object
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = player.transform.position;

            // Calculate the distance
            float distance = Vector3.Distance(currentPosition, targetPosition);

            if (distance < 2) {
                // Moves faster the closer you are
                var speed = 4 - distance*2;
                var step = speed*Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collision Damage
        var target = other.GetComponent<LevelUp>(); 
        if (target != null)
        {
            target.experience += XPAmount;
            Destroy(gameObject);
        }
    }
}
