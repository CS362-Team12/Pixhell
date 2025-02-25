using UnityEngine;
using UnityEngine.InputSystem;

public class ArcherAbility : MonoBehaviour
{
    [Header("Input Actions")]
    public InputAction ArrowVolley;
    //public InputAction SnareArrow;
    public InputAction PiercingArrow;

    [Header("Arrow Volley Setting")]
    float volley_cooldown = 8.0f;
    float piercing_cooldown = 2.0f;
    float volley_time = -2f;
    float piercing_time = -2f;

    public GameObject ProjectilePrefab;
    public GameObject PiercingPrefab;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        ArrowVolley.Enable();
        PiercingArrow.Enable();
        //SnareArrow.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrowVolley.IsPressed())
        {
            FireArrowVolley();
        }

        if (PiercingArrow.IsPressed())
        {
            FirePiercingArrow();
        }
    }

    void FireArrowVolley()
    {
        if (Time.time - volley_time >= volley_cooldown)
        {
            volley_time = Time.time;
            for (int i = 0; i < 10; i++)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;

                GameObject projectileObject = Instantiate(ProjectilePrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                Projectile projectile = projectileObject.GetComponent<Projectile>();

                projectile.Launch(direction, 6.5f);
            }
        }
    }

    void FirePiercingArrow()
    {
        if (Time.time - piercing_time >= piercing_cooldown)
        {
            piercing_time = Time.time;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;

            GameObject projectileObject = Instantiate(PiercingPrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
            PiercingArrow projectile = projectileObject.GetComponent<PiercingArrow>();

            projectile.Launch(direction, 10.0f);
        }
    }
}
