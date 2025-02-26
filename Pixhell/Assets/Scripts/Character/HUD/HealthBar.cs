using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthBar : MonoBehaviour
{

    public Sprite healthChunk;
    public Image healthBar;
    PlayerController character;

    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        healthBar = GetComponent<Image>();
        GameObject characterObj = GameObject.Find("walk-with-weapon-1");
        character = characterObj.GetComponent<PlayerController>();
    }

    void Update() {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log(scene.name);
        if (scene.name == "StartMenu" || scene.name == "SelectRun") {
            healthBar.enabled = false;
        }
        else {
            healthBar.enabled = true;
        }
    }

}