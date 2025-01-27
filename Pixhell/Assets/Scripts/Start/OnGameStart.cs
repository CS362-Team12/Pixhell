using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameStart : MonoBehaviour
{
    void Start()
    {
        Debug.Log("LOADING");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}