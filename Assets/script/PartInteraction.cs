using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class  PartInteraction : MonoBehaviour
{
    public GameObject infoPanel; // Assign the info panel in the Inspector.

    private ARRaycastManager raycastManager;

    void Start()
    {
        raycastManager = Object.FindFirstObjectByType<ARRaycastManager>(); // Updated method
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouch(touch);
            }
        }
    }

    void HandleTouch(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                ShowInfoPanel();
            }
        }
    }

    void ShowInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true); // Show panel
        }
    }
}
