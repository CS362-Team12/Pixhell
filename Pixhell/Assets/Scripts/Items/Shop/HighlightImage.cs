using UnityEngine;

public class HighlightImage : MonoBehaviour
{
    public Sprite defaultImage;
    public Sprite hoverImage;
    private SpriteRenderer iconRenderer;
    private BoxCollider2D collider2D;
    private ItemShop shop;

    void Start()
    {
        iconRenderer = GetComponentInChildren<SpriteRenderer>();
        collider2D = GetComponentInChildren<BoxCollider2D>();
        iconRenderer.sprite = defaultImage; 
        shop = GetComponentInChildren<ItemShop>();
    }

    void Update()
    {
        if (IsMouseOver() && !shop.shopShowing)
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
        // Get the collider's world-space bounds
        Vector3 colliderWorldPos = collider2D.transform.position;
        Vector3 colliderSize = collider2D.bounds.size;
        Vector3 topLeft = Camera.main.WorldToScreenPoint(colliderWorldPos - (colliderSize / 2));
        Vector3 bottomRight = Camera.main.WorldToScreenPoint(colliderWorldPos + (colliderSize / 2));
        Vector3 mousePos = Input.mousePosition;

        // Check if the mouse is inside the collider's screen-space bounds
        if (mousePos.x >= topLeft.x && mousePos.x <= bottomRight.x &&
            mousePos.y >= topLeft.y && mousePos.y <= bottomRight.y)
        {
            return true;
        }

        return false;
    }
}
