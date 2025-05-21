using UnityEngine;

public class HighlightOnClick : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow; // Change this to any highlight color

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    void OnMouseDown()
    {
        objectRenderer.material.color = highlightColor;
        Invoke("ResetColor", 1f); // Reset after 1 second
    }

    void ResetColor()
    {
        objectRenderer.material.color = originalColor;
    }
}
