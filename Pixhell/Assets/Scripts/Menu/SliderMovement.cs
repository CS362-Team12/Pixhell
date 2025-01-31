using UnityEngine;
using UnityEngine.UI;

public class SliderMoveObject : MonoBehaviour
{
    [SerializeField] private Slider slider;              // Reference to the slider
    [SerializeField] private RectTransform objectToMove;  // Reference to the object to move

    [SerializeField] private float maxMovement = 500f;    // Max X position for movement
    private float initialPositionX;  // To store the initial X position of the object

    void Start() {
        // Store the initial X position of the object
        initialPositionX = objectToMove.localPosition.x;
    }

    void Update()
    {
        // Calculate the new X position based on slider value
        float targetX = initialPositionX + slider.value * maxMovement;

        // Update the object's position smoothly by modifying only the X component
        objectToMove.localPosition = new Vector3(targetX, objectToMove.localPosition.y, objectToMove.localPosition.z);
    }
}