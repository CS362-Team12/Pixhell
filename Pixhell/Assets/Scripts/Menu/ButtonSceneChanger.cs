//This class allows the scene to change when clicking a button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonSceneChanger : MonoBehaviour {
    [SerializeField] string sceneName;
    public Button button;
    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        SceneManager.LoadScene(sceneName);
    }

}