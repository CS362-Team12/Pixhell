using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    public GameObject archerVariant;
    public GameObject warriorVariant;
    public GameObject mageVariant;

    string character = "NONE";

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("Updating Character, new scene");
        UpdateCharacter();
    }

    public void UpdateCharacter() {
        GameObject player = GameObject.FindWithTag("Player");
        if (player) {
            // Instantiate variant and destory old character
            switch(character) {
                case "Archer":
                    Destroy(GameObject.Find("WarriorVariant"));
                    Destroy(GameObject.Find("MageVariant"));
                    //Debug.Log("Creating Archer Variant");
                    //Instantiate(archerVariant, player.transform.position, player.transform.rotation);
                    break;
                case "Warrior":
                    Destroy(GameObject.Find("ArcherVariant"));
                    Destroy(GameObject.Find("MageVariant"));
                    //Debug.Log("Creating Warrior Variant");
                    //Instantiate(warriorVariant, player.transform.position, player.transform.rotation);
                    break;
                case "Mage":
                    Destroy(GameObject.Find("ArcherVariant"));
                    Destroy(GameObject.Find("WarriorVariant"));
                    //Debug.Log("Creating Mage Variant");
                    //Instantiate(mageVariant, player.transform.position, player.transform.rotation);
                    break;
                default:
                    Debug.LogError("CHARACTER NOT FOUND: SETTING TO ARCHER AS DEFAULT");
                    Destroy(GameObject.Find("WarriorVariant"));
                    Destroy(GameObject.Find("MageVariant"));
                    //Debug.Log("Creating Archer Variant");
                    //Instantiate(archerVariant, player.transform.position, player.transform.rotation);
                    break;
            }
            //Debug.Log("Destorying old player: ");
            //Debug.Log(player);
            //Destroy(player);
        }
    }

    public void SetCharacter(string c) {
        Debug.Log("Setting character to " + c);
        character = c;
        UpdateCharacter();
    }
}
