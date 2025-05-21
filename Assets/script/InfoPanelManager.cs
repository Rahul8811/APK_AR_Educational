using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour
{
    public GameObject infoPanel;  // Reference to the info panel UI
    public Button infoButton;     // Button to show the info panel
    public Button backButton;     // Button to hide the info panel

    void Start()
    {
        infoPanel.SetActive(false); // Hide info panel at the start

        // Add event listeners to buttons
        infoButton.onClick.AddListener(ShowInfo);
        backButton.onClick.AddListener(HideInfo);
    }

    void ShowInfo()
    {
        infoPanel.SetActive(true); // Show the panel
    }

    void HideInfo()
    {
        infoPanel.SetActive(false); // Hide the panel
    }
}
