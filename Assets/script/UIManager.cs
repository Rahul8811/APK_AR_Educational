using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject infoPanel;    // Assign in Inspector
    public GameObject mainBackButton;  // Back button on the main screen
    public GameObject infoButton;   // Info button on the main screen
    public GameObject infoBackButton; // Back button inside the info panel

    private bool isInfoPanelActive = false;

    public void ToggleInfoPanel()
    {
        isInfoPanelActive = !isInfoPanelActive;

        // Toggle visibility properly
        infoPanel.SetActive(isInfoPanelActive);
        mainBackButton.SetActive(!isInfoPanelActive);  // Hide main back button when info panel is open
        infoBackButton.SetActive(isInfoPanelActive);   // Show back button inside the info panel
        infoButton.SetActive(!isInfoPanelActive);      // Hide info button when panel is open
    }

    public void CloseInfoPanel()
    {
        // Reset visibility when closing the info panel
        isInfoPanelActive = false;
        infoPanel.SetActive(false);
        mainBackButton.SetActive(true);   // Show main back button again
        infoBackButton.SetActive(false);  // Hide info panel's back button
        infoButton.SetActive(true);       // Show info button again
    }
}
