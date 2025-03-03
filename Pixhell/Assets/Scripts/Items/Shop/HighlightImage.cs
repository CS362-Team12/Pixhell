using UnityEngine;

public class HighlightImage : MonoBehaviour
{
    public Sprite defaultImage;
    public Sprite hoverImage;
    private SpriteRenderer iconRenderer;
    private BoxCollider2D collider2D;
    private ItemShop shop;

    PauseController pauseController;

    void Start()
    {
        iconRenderer = GetComponentInChildren<SpriteRenderer>();
        collider2D = GetComponentInChildren<BoxCollider2D>();
        iconRenderer.sprite = defaultImage; 
        shop = GetComponentInChildren<ItemShop>();
        pauseController = GameObject.Find("EventSystem").GetComponent<PauseController>();
    }

    void Update()
    {
        if (IsMouseOver() && !shop.shopShowing && !pauseController.isPaused)
        {
            iconRenderer.sprite = hoverImage;
        }
        else
        {
            iconRenderer.sprite = defaultImage;
        }
    }

    bool IsMouseOver()
    // Finds whether mouse is in same location as sprite
    {
        Vector3 colliderWorldPos = collider2D.transform.position;
        Vector3 colliderSize = collider2D.bounds.size;
        Vector3 topLeft = Camera.main.WorldToScreenPoint(colliderWorldPos - (colliderSize / 2));
        Vector3 bottomRight = Camera.main.WorldToScreenPoint(colliderWorldPos + (colliderSize / 2));
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x >= topLeft.x && mousePos.x <= bottomRight.x &&
            mousePos.y >= topLeft.y && mousePos.y <= bottomRight.y)
        {
            return true;
        }

        return false;
    }
}
