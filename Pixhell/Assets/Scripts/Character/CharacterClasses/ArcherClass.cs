using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArcherClass : PlayerController
{
    public GameObject projectilePrefab;
    float special_1_cooldown = 14f;
    float special_1_time;
    bool special_1_on_cooldown = false;
    Image PiercingImage;


    float special_2_cooldown = 10f;
    float special_2_time;
    bool special_2_on_cooldown = false;
    int volley_arrow_count;
    Image VolleyImage;

    public GameObject PiercingPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        max_health *= .75f;
        current_health = max_health;
        damage *= .60f;
        speed_mult *= 1.2f;
        attack_speed_mult *= 1.1f;

        PiercingImage = GameObject.Find("SpecialOneOnCooldown").GetComponent<Image>();
        PiercingImage.fillAmount = 0f;
        special_1_time = -special_1_cooldown;

        VolleyImage = GameObject.Find("SpecialTwoOnCooldown").GetComponent<Image>();
        VolleyImage.fillAmount = 0f;
        special_2_time = -special_2_cooldown;

        volley_arrow_count = 5;

        GameObject test = GameObject.FindWithTag("IconManager");
        test.GetComponent<IconManager>().InsertIcon("Archer");
    }

    public override void ResetPlayerStats()
    {
        Start();
    }

    protected override void Update()
    {
        base.Update();
        if (SpecialOne.IsPressed())
        {
            Special1();
            special_1_on_cooldown = true;
        }
        if (special_1_on_cooldown)
        {
            PiercingImage.fillAmount = (special_1_cooldown - Time.time + special_1_time) / special_1_cooldown;
            if (PiercingImage.fillAmount == 0f)
            {
                special_1_on_cooldown = false;
            }
        }
        if (SpecialTwo.IsPressed())
        {
            Special2(volley_arrow_count);
            special_2_on_cooldown = true;
        }
        if (special_2_on_cooldown)
        {
            VolleyImage.fillAmount = (special_2_cooldown - Time.time + special_2_time) / special_2_cooldown;
            if (VolleyImage.fillAmount == 0f)
            {
                special_2_on_cooldown = false;
            }
        }
    }
    protected override void BasicAttack(Vector2 move)
    {
        if ((!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        || (SprintAction.IsPressed() && stopTime >= minStopDuration && !DodgeAction.IsPressed()))
        {
            if (Time.time - attack_time >= attack_speed / attack_speed_mult)
            {
                animator.SetTrigger("Attack");
                attack_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;

                GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                ArcherProjectile projectile = projectileObject.GetComponent<ArcherProjectile>();

                projectile.Launch(direction, 6.5f, projectile_speed_mult, damage, damage_mult);
            }
        }
    }

    protected void Special1()
    {
        if ((!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        || (SprintAction.IsPressed() && stopTime >= minStopDuration && !DodgeAction.IsPressed()))
        {
            if (Time.time - special_1_time >= special_1_cooldown)
            {
                special_1_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;

                GameObject projectileObject = Instantiate(PiercingPrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                PiercingArrow projectile = projectileObject.GetComponent<PiercingArrow>();

                projectile.Launch(direction, 10f, projectile_speed_mult, damage + 10, damage_mult);
            }
        }
    }

    protected void Special2(int arrow_amount)
    {
        if ((!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        || (SprintAction.IsPressed() && stopTime >= minStopDuration && !DodgeAction.IsPressed()))
        {
            if (Time.time - special_2_time >= special_2_cooldown)
            {
                special_2_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;
                float x = direction.x;
                float y = direction.y;
                float angle = 60 / (arrow_amount - 1) * -Mathf.PI / 180;

                // PI/6 = 30 degrees, clockwise and counter-clockwise, for a total of 60 degrees cone
                Vector2 arrow_direction = new(x * Mathf.Cos(Mathf.PI / 6f) - y * Mathf.Sin(Mathf.PI / 6f), x * Mathf.Sin(Mathf.PI / 6f) + y * Mathf.Cos(Mathf.PI / 6f));

                for (int i = 0; i < arrow_amount; i++)
                {
                    GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                    ArcherProjectile projectile = projectileObject.GetComponent<ArcherProjectile>();
                    projectile.Launch(arrow_direction, 6.5f, projectile_speed_mult, damage, damage_mult);
                    x = arrow_direction.x;
                    y = arrow_direction.y;
                    arrow_direction = new Vector2(x * Mathf.Cos(angle) - y * Mathf.Sin(angle), x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
                }
            }
        }
    }
}
