using UnityEngine;
using UnityEngine.Assertions.Must;

public class MageHomingMissile : MonoBehaviour
{
    public GameObject target;

    float rotateSpeed = 30f;
    float damage;

    Rigidbody2D rigidbody2d;
    public Vector2 direction;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (target != null)
        {
            rotateSpeed = (rotateSpeed <= 180f) ? rotateSpeed += Time.deltaTime * 60f : 180f;
            Vector2 optimalDirection = ((Vector2)(target.transform.position - transform.position)).normalized;
            Debug.LogWarning(optimalDirection);
            float targetAngle = Mathf.Atan2(optimalDirection.y, optimalDirection.x) * Mathf.Rad2Deg;
            float currentAngle = transform.eulerAngles.z;
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
            float rotationStep = Mathf.Clamp(angleDifference, -rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle + rotationStep);
            rigidbody2d.linearVelocity = transform.right * rigidbody2d.linearVelocity.magnitude;
        }
    }

    public void Launch(Vector2 direction, float force, float force_mult, float dam, float dam_mult)
    {
        rigidbody2d.AddForce(direction.normalized * (force * force_mult), ForceMode2D.Impulse);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = dam * dam_mult;
    }

    public void setTarget(GameObject enemyTarget)
    {
        target = enemyTarget;
        Debug.LogWarning(target);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);  // Call the TakeDamage method
            Destroy(gameObject);
        }    
    }
}
