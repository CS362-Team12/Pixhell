using TMPro;  // Make sure you have this at the top for TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool showLabel = true;


    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (text != null) {
            if (showLabel && text) {
                text.text = "Coins: " + GameManager.coins.ToString();
            }
            else {
                text.text = GameManager.coins.ToString();
            }
        }
        
        
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (text != null) {
            GameObject obj = this.gameObject;
            obj.SetActive(false);
            if (scene.name != "StartMenu" && scene.name != "SelectRun" && scene.name != "CharacterSelect") {
                obj.SetActive(true);
            }
        }
        
    
    }
}
