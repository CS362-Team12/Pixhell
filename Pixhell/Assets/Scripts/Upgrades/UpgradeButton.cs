using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour
{
    public int upgradeNumber;
    Button button;

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick() {
        GameObject upgradeController = GameObject.Find("EventSystem");
        upgradeController.GetComponent<UpgradeController>().ChooseUpgrade(upgradeNumber-1);
    }
}