using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 100f; // Controls how fast the object rotates
    private bool isRotating = false;   // Keeps track of whether the user is rotating the object
    private Vector2 lastMousePosition; // Stores the last touch/click position

    void Update()
    {
        // When the user touches the screen (or clicks the mouse)
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true; // Start rotating
            lastMousePosition = Input.mousePosition; // Store the touch position
        }
        // When the user lifts their finger (or releases the mouse click)
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false; // Stop rotating
        }

        // If the user is still touching the screen, rotate the object
        if (isRotating)
        {
            // Calculate how much the finger moved
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;

            // Rotate the object around its own Y-axis based on finger movement
            transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime, Space.Self);

            // Update the last touch position
            lastMousePosition = Input.mousePosition;
        }
    }
}
