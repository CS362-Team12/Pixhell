using UnityEngine;

public class ShopIconHover : MonoBehaviour
{
    public Sprite defaultImage;
    public Sprite hoverImage;
    private SpriteRenderer iconRenderer;
    private BoxCollider2D collider2D;
    private ItemShop shop;

    void Start()
    {
        iconRenderer = GetComponentInChildren<SpriteRenderer>();
        collider2D = GetComponentInChildren<BoxCollider2D>(); // Assuming the collider is a BoxCollider2D
        iconRenderer.sprite = defaultImage;  // Set initial sprite
        shop = GetComponentInChildren<ItemShop>();
    }

    void Update()
    {
        if (IsMouseOver() && !shop.shopShowing)
        {
            iconRenderer.sprite = hoverImage;  // Highlight the icon on hover
        }
        else
        {
            iconRenderer.sprite = defaultImage;  // Reset the icon when not hovering
        }
    }

    bool IsMouseOver()
    {
        // Get the collider's world-space bounds
        Vector3 colliderWorldPos = collider2D.transform.position;
        Vector3 colliderSize = collider2D.bounds.size;

        // Convert world-space bounds to screen-space
        Vector3 topLeft = Camera.main.WorldToScreenPoint(colliderWorldPos - (colliderSize / 2));
        Vector3 bottomRight = Camera.main.WorldToScreenPoint(colliderWorldPos + (colliderSize / 2));

        // Get the mouse position in screen-space
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
