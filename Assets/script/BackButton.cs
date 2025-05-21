using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject panel; // Assign the info panel.

    public void HidePanel()
    {
        panel.SetActive(false); // Hide panel
    }
}
