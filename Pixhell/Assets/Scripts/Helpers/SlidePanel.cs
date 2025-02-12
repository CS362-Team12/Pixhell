using UnityEngine;
using UnityEngine.UI;

public class SlidePanel : MonoBehaviour
{
    public Scrollbar scrollbar; // Reference to the scrollbar
    public RectTransform panelContainer; // The container holding the run buttons
    

    void Start()
    {
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
        }
    }

    // This function gets called whenever the scrollbar value changes
    void OnScrollbarValueChanged(float value)
    {
        float newPosition = Mathf.Lerp(0, panelContainer.rect.width, value);
        panelContainer.anchoredPosition = new Vector2(-1 * newPosition, panelContainer.anchoredPosition.y);
    }

    void OnDestroy()
    {
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
        }
    }
}