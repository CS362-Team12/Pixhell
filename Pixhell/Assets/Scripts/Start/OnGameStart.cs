using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameStart : MonoBehaviour
{
    void Start()
    {
        Debug.Log("LOADING");
        SceneManager.LoadScene("GlobalScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}