using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class InteractiveFloater : MonoBehaviour
{
    public GameObject infoPanel; // Reference to the UI panel
    public Text infoText; // Reference to the info text
    private ARRaycastManager raycastManager;

    private void Start()
    {
        raycastManager = Object.FindFirstObjectByType<ARRaycastManager>();
        infoPanel.SetActive(false); // Hide info panel on start
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) // For play mode
        {
            CheckInteraction(Input.mousePosition);
        }
#else
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) // For mobile/AR
        {
            CheckInteraction(Input.GetTouch(0).position);
        }
#endif
    }

    void CheckInteraction(Vector2 inputPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Floater"))
            {
                ShowInfo(hit.collider.GetComponent<FloaterData>());
            }
        }
    }

    void ShowInfo(FloaterData floater)
    {
        if (floater != null)
        {
            infoText.text = floater.info; // Display information
            infoPanel.SetActive(true); // Show the panel
        }
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false); // Hide the panel
    }
}
