// WILL NEED UPDATING TO BE INTERACTIVE

using UnityEngine;

public class ItemShop : MonoBehaviour
{
    GameObject itemShopUI;
    bool shopShowing;

    void Start() {
        itemShopUI = transform.Find("ItemShopUI").gameObject;
        shopShowing = false;
        itemShopUI.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!shopShowing) {
            ToggleUI();
        }
    }

    public void ToggleUI()
    // Toggles the shop menu
    {
        Debug.Log("Toggled Shop");
        shopShowing = !shopShowing;
        itemShopUI.SetActive(shopShowing);  // Show/hide the shop
        Time.timeScale = !shopShowing ? 1f : 0f;  // Freeze gameplay time when shop open
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) { // Ensure that the scene has a player obj
            player.GetComponent<PlayerController>().enabled = !shopShowing ? true: false;
        }
    }
}