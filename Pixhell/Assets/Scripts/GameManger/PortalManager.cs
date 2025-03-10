// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    public float sceneNumber = 10;
    public Sprite isActiveImage;
    public Sprite notActiveImage;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip teleportSound;

    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        if (sceneNumber <= GameManager.maxArena)
        {
            render.sprite = isActiveImage;
        }
        else{
            render.sprite = notActiveImage;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // animator = GetComponent<Animator>();
        
        if (other.CompareTag("Player") && sceneNumber <= GameManager.maxArena)
        {   
            Animator animator = other.GetComponent<Animator>();
            if (!animator.GetBool("is_teleporting"))
            {
                animator.SetBool("is_teleporting", true);
                Debug.Log("TELEPORTING");

                if (teleportSound != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySoundEffect(teleportSound, 0.04f);
                }

                GameObject player = GameObject.FindWithTag("Player");
                player.transform.position = transform.position + new Vector3(0, 0.25f, 0);
                player.GetComponent<PlayerController>().SetIsDodging(false);
                StartCoroutine(LoadSceneAfterAnimation(animator));
            }
        }
        // Debug.Log(animator.is_teleporting);
    }

    private IEnumerator LoadSceneAfterAnimation(Animator animator)
    {
        // Get the length of the teleport animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationDuration);

        // Load the scene
        SceneManager.LoadScene(sceneToLoad);
    }
}