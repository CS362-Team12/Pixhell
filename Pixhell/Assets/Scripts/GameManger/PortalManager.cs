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
        if (other.CompareTag("Player") && !animator.GetBool("is_teleporting"))
        {
            animator.SetBool("is_teleporting", true);
            Debug.Log("TELEPORTING");
            GameObject player = GameObject.Find("walk-with-weapon-1");
            player.transform.position = transform.position;
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





/*
// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;
using UnityEngine.SceneManagement;

public Animator animator;
public class Portal : MonoBehaviour
{
    public string sceneToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        animator = GetComponent<Animator>();
        if (other.CompareTag("Player"))
        {
            animator.SetBool("is_teleporting", true);
            
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
*/