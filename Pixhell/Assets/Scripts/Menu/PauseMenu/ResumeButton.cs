using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

    
public class ResumeButton : MonoBehaviour {
    public Button button;
    PauseController pauseController;
    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        pauseController = GetComponentInParent<PauseController>();
    }

    void OnClick() {
        pauseController.TogglePause();
    }

}