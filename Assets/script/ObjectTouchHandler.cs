using UnityEngine;

public class ObjectTouchHandler : MonoBehaviour
{
    private Color originalColor;
    private Renderer objectRenderer;

    void Start()
    {
        // Cache the object's renderer and save the original color
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    void OnMouseDown()
    {
        Debug.Log("Touched: " + gameObject.name);

        // Rotate the object
        RotateObject();

        // Highlight the object
        HighlightObject();

        // Scale the object slightly
        ScaleObject();
    }

    void RotateObject()
    {
        transform.Rotate(0, 0, 0); // Rotates 45 degrees on Y-axis
    }

    void HighlightObject()
    {
        objectRenderer.material.color = Color.yellow; // Change color on touch
        Invoke("ResetColor", 1f); // Reset color after 1 second
    }

    void ScaleObject()
    {
        transform.localScale *= 1.1f; // Increase scale by 10%
        Invoke("ResetScale", 1f); // Reset scale after 1 second
    }

    void ResetColor()
    {
        objectRenderer.material.color = originalColor; // Restore original color
    }

    void ResetScale()
    {
        transform.localScale /= 1.1f; // Restore original scale
    }
}
