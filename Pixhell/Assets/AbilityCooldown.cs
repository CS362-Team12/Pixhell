using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class AbilityCooldown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject characterObj = GameObject.FindWithTag("Player");
        gameObject.SetActive(false);
        if (characterObj != null)
        {
            if (scene.name != "StartMenu" && scene.name != "SelectRun")
            {
                gameObject.SetActive(true);
            }
        }

    }
}
