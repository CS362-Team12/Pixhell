// Used in pair with the add new run button in the Run Selection
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewRunButton : MonoBehaviour {
    public Button button;
    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        //LoadCharacterSaves.MakeFreshRun();
    }

}