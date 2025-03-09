using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MageClass : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject projectilePrefab;

    float special_1_cooldown = 2f;
    float special_1_time;
    bool special_1_on_cooldown = false;
    Image LightningImage;


    float special_2_cooldown = 2f;
    float special_2_time;
    bool special_2_on_cooldown = false;
    public int missile_count;
    
    public GameObject MageHomingMissile;
    Image HomingImage;

    public ChainLightning chainLightning;


    protected override void Start()
    {
        base.Start();
        max_health *= .75f;
        current_health = max_health;
        damage *= .48f;
        speed_mult *= 1.2f;
        attack_speed_mult *= 1.15f;

        LightningImage = GameObject.Find("SpecialOneOnCooldown").GetComponent<Image>();
        LightningImage.fillAmount = 0f;
        special_1_time = -special_1_cooldown;

        HomingImage = GameObject.Find("SpecialTwoOnCooldown").GetComponent<Image>();
        HomingImage.fillAmount = 0f;
        special_2_time = -special_2_cooldown;

        missile_count = 6;

        GameObject test = GameObject.FindWithTag("IconManager");
        test.GetComponent<IconManager>().InsertIcon("Mage");
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
            LightningImage.fillAmount = (special_1_cooldown - Time.time + special_1_time) / special_1_cooldown;
            if (LightningImage.fillAmount == 0f)
            {
                special_1_on_cooldown = false;
            }
        }
        if (SpecialTwo.IsPressed())
        {
            Special2();
            special_2_on_cooldown = true;
        }
        if (special_2_on_cooldown)
        {
            HomingImage.fillAmount = (special_2_cooldown - Time.time + special_2_time) / special_2_cooldown;
            if (HomingImage.fillAmount == 0f)
            {
                special_2_on_cooldown = false;
            }
        }
    }

    public override void ResetPlayerStats()
    {
        Start();
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
                MageProjectile projectile = projectileObject.GetComponent<MageProjectile>();
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
                Instantiate(chainLightning, mousePosition, Quaternion.identity);
            }
        }
    }

    protected void Special2()
    {
        if ((!SprintAction.IsPressed() && !DodgeAction.IsPressed())
        || (SprintAction.IsPressed() && stopTime >= minStopDuration && !DodgeAction.IsPressed()))
        {
            if (Time.time - special_2_time >= special_2_cooldown)
            {
                special_2_time = Time.time;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject target = FindClosestEnemy(mousePosition);
                mousePosition.z = 0f;
                Vector2 direction = ((Vector2)(mousePosition - transform.position)).normalized;
                float x = direction.x;
                float y = direction.y;
                float angle = 90 / (6 - 1) * -Mathf.PI / 180;

                // PI/6 = 45 degrees, clockwise and counter-clockwise, for a total of 90 degrees cone
                Vector2 missile_direction = new(x * Mathf.Cos(Mathf.PI / 4f) - y * Mathf.Sin(Mathf.PI / 4f), x * Mathf.Sin(Mathf.PI / 4f) + y * Mathf.Cos(Mathf.PI / 4f));
                
                for (int i = 0; i < 6; i++)
                {
                    GameObject projectileObject = Instantiate(MageHomingMissile, rigidbody2d.position + Vector2.up * .15f, Quaternion.identity);
                    projectileObject.GetComponent<MageHomingMissile>().setTarget(target);
                    MageHomingMissile projectile = projectileObject.GetComponent<MageHomingMissile>();
                    projectile.Launch(missile_direction, 8f, projectile_speed_mult, damage, damage_mult);
                    x = missile_direction.x;
                    y = missile_direction.y;
                    missile_direction = new Vector2(x * Mathf.Cos(angle) - y * Mathf.Sin(angle), x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
                }
            }
        }
    }

    // https://stackoverflow.com/questions/61989147/i-want-to-find-automatically-enemies-and-put-a-target-on-the-closest-of-them-un
    public GameObject FindClosestEnemy(Vector3 mousePosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If no enemies found at all directly return nothing
        // This happens if there simply is no object tagged "Enemy" in the scene
        if (enemies.Length == 0)
        {
            Debug.LogWarning("No enemies found!", this);
            return null;
        }

        GameObject closest;

        // If there is only exactly one anyway skip the rest and return it directly
        if (enemies.Length == 1)
        {
            closest = enemies[0];
            return closest;
        }


        // Otherwise: Take the enemies
        closest = enemies
            // Order them by distance (ascending) => smallest distance is first element
            .OrderBy(go => (mousePosition - go.transform.position).sqrMagnitude)
            .First();

        return closest;
    }
}
