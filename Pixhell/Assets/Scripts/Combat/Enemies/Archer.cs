using UnityEngine;

public class Archer : Enemy
{
    public float orbitRadius;  // The distance between the two objects
    public float orbitSpeed;  // Speed in which its oribit position changes

    float angle = 0f;   // Angle in radians, used for calculating the orbit

    public override void Move() {
        var player = GameObject.FindWithTag("Player");
        angle += orbitSpeed * Time.deltaTime;

        // Calculate the new position using trig
        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;

        // Update the position of the object (relative to the target's position)
        Vector3 target = new Vector3(player.transform.position.x + x, player.transform.position.y + y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
