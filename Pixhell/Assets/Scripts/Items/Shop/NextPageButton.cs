using UnityEngine;
using UnityEngine.UI;

public class NextPageButton : MonoBehaviour
{
    RectTransform rect; // The ScrollRect containing the panel
    ItemShop shop;

    void Start()
    {
        // Add listener to the button's OnClick event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        shop = GetComponentInParent<ItemShop>();
        
    }
    
    void OnButtonClick()
    {
        rect = transform.parent.Find("ItemsPanel").GetComponent<RectTransform>();
        if ((rect.anchoredPosition.y / shop.pageSpacing) < Mathf.CeilToInt((float)shop.count / 6) - 1) {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + shop.pageSpacing);
        }
        
    }
}
