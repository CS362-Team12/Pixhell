//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonCharacterSelect : MonoBehaviour {
    public Button button;
    public string character;
    GameObject eventSystem;
    // Start is called before the first frame update
    void Start() {
        eventSystem = GameObject.Find("EventSystem");
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        if (character == "")
            Debug.LogError("Missing character name. Attach character name to button in inspector.");
        else {
            eventSystem.GetComponent<CharacterSelectController>().SetCharacter(character);
        }

        GameManager.SavePlayerData();
        SceneManager.LoadScene("Limbo");
    }

}