using System.Security.Cryptography;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;


public class WarriorClass : PlayerController
{
    
    protected float attack_range = 1.7f;
    public LayerMask enemyLayers;

    [Header("Warrior Audio")]
    [SerializeField] private AudioClip warriorAttackSound;

    protected override void Start()
    {
        base.Start();
        max_health *= 1.25f;
        current_health = max_health;
        attack_speed *= 1.2f;
        speed_mult *= 1f;
    }

    protected override void Update()
    {
        base.Update();
        
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

                if (warriorAttackSound != null)
                {
                    AudioManager.Instance.PlaySoundEffect(warriorAttackSound, 0.6f);
                    Debug.Log("Warrior attack sound played: " + warriorAttackSound.name);
                }
                else
                {
                    Debug.LogWarning("Warrior attack sound not assigned");
                }

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 attackDirection = (mousePosition - transform.position).normalized;
                Debug.DrawLine(transform.position, transform.position + (Vector3)attackDirection * attack_range, Color.red, .2f);
                float attack_angle = 120f;



                GameObject slash_animation = Instantiate(slash_prefab, transform.position + (Vector3)attackDirection.normalized * attack_range, Quaternion.identity);

                // slash_animation.transform.position += 30 * attack_range * (Vector3)attackDirection; 
                if (!m_FacingRight)
                {
                    Vector3 scale = slash_animation.transform.localScale;
                    scale.y *= -1;
                    slash_animation.transform.localScale = scale; 
                }

                float slash_angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                slash_animation.transform.rotation = Quaternion.Euler(0, 0, slash_angle);


                StartCoroutine(KillSlash(slash_animation));

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attack_range, enemyLayers);
                
                foreach (Collider2D enemy in hitEnemies)
                {
                    Vector2 enemyDirection = (enemy.transform.position - transform.position).normalized;
                    float angle = Vector2.SignedAngle(attackDirection, enemyDirection);

                    // Check if enemy is within attack arc
                    if (Mathf.Abs(angle) <= attack_angle / 2)
                    {
                        Enemy target = enemy.GetComponent<Enemy>();
                        if (target != null)
                        {
                            target.TakeDamage(damage * damage_mult);
                        }
                    }
                }
            }
        }
    }

    private IEnumerator KillSlash(GameObject slash)
    {
        // Get the length of the slash animation
        Animator anim = slash.GetComponent<Animator>();
        anim.SetFloat("AnimationSpeed", attack_speed_mult);
        float animationDuration = anim.GetCurrentAnimatorStateInfo(0).length / attack_speed_mult;
        
        // Wait for the animation to finish
        yield return new WaitForSeconds(animationDuration);

        Destroy(slash);
    }
}
