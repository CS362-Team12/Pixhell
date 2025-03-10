using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    private CircleCollider2D collider;
    public LayerMask enemyLayer;
    public float damage;

    public GameObject chainLightningFX;
    public GameObject beenStruck;

    public int chain_amount;

    private GameObject startObject;
    public GameObject endObject;

    private Animator animator;

    public ParticleSystem particle;

    private int single_spawn;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip chainLightningSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (chain_amount == 0)
        {
            Destroy(gameObject);
        }
        single_spawn = 1;
        collider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
        startObject = gameObject;
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<Enemy>();
        if (other.CompareTag("Enemy") && !other.GetComponentInChildren<EnemyStruck>())
        {
            if (single_spawn != 0)
            {
                single_spawn -= 1;
                endObject = other.gameObject;
                chain_amount -= 1;

                Instantiate(chainLightningFX, other.gameObject.transform.position, Quaternion.identity);

                Instantiate(beenStruck, other.gameObject.transform);

                target.TakeDamage(damage);

                if (chainLightningSound != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySoundEffect(chainLightningSound, 0.03f);
                }

                animator.StopPlayback();
                collider.enabled = false;
                particle.Play();

                var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = startObject.transform.position;

                particle.Emit(emitParams, 1);

                emitParams.position = endObject.transform.position;

                particle.Emit(emitParams, 1);

                emitParams.position = (startObject.transform.position + endObject.transform.position) / 2;

                particle.Emit(emitParams, 1);

                Destroy(gameObject, 1f);
            }
        }
    }
}
