// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    public Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        // animator = GetComponent<Animator>();
        if (other.CompareTag("Player") && (!animator || !animator.GetBool("is_teleporting")))
        {   
            animator = other.GetComponent<Animator>();
            Debug.Log(animator);
            animator.SetBool("is_teleporting", true);
            Debug.Log("TELEPORTING");
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = transform.position + new Vector3(0, 0.25f, 0);
            StartCoroutine(LoadSceneAfterAnimation());
        }
        // Debug.Log(animator.is_teleporting);
    }

    private IEnumerator LoadSceneAfterAnimation()
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