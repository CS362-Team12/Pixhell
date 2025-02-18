using UnityEngine;
using UnityEngine.InputSystem;

public class ArcherAbility : MonoBehaviour
{
    [Header("Input Actions")]
    public InputAction ArrowVolley;
    public InputAction SnareArrow;

    [Header("Arrow Volley Setting")]
    float Cooldown = 8.0f;
    float ability_time = -2f;

    public GameObject projectilePrefab;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        ArrowVolley.Enable();
        SnareArrow.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrowVolley.IsPressed())
        {
            FireArrowVolley();
        }
    }

    void FireArrowVolley()
    {
        if (Time.time - ability_time >= Cooldown)
        {
            ability_time = Time.time;
            for (int i = 0; i < 10; i++)
            {
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
