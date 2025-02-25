using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

    
public class CloseItemShopButton : MonoBehaviour {
    public Button button;
    ItemShop itemShop;

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        itemShop = transform.parent.parent.GetComponent<ItemShop>();
    }

    void OnClick() {
        itemShop.ToggleUI();
    }

}