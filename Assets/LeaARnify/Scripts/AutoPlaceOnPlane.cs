using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlaceOnPlane : MonoBehaviour
{
    [Header("Placement Settings")]
    [Tooltip("The object to place on detected planes")]
    public GameObject objectToPlace;

    [Tooltip("Distance in front of camera to cast ray")]
    public float placementDistance = 2.0f;

    [Tooltip("Should we disable plane detection after placement?")]
    public bool disablePlaneDetectionAfterPlacement = true;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private bool objectPlaced = false;
    private GameObject placedInstance;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        if (objectToPlace == null)
        {
            Debug.LogError("Object to place is not assigned!", this);
            enabled = false;
        }
    }

    void Update()
    {
        if (objectPlaced || objectToPlace == null) return;

        // Create a ray from camera position forward
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Perform the raycast
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            // Only instantiate once
            if (placedInstance == null)
            {
                placedInstance = Instantiate(objectToPlace, pose.position, pose.rotation);

                // Create anchor to maintain world position
                placedInstance.AddComponent<ARAnchor>();

                // Align with camera forward but keep Y rotation level
                Vector3 lookDirection = Camera.main.transform.forward;
                lookDirection.y = 0; // Keep object level
                placedInstance.transform.rotation = Quaternion.LookRotation(lookDirection);
            }
            else
            {
                // Update position if already instantiated
                placedInstance.transform.position = pose.position;
            }

            // Finalize placement when conditions are right
            if (hits[0].distance < placementDistance)
            {
                objectPlaced = true;

                if (disablePlaneDetectionAfterPlacement)
                {
                    DisablePlaneVisualization();
                }
            }
        }
    }

    private void DisablePlaneVisualization()
    {
        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    // Public method to reset placement
    public void ResetPlacement()
    {
        objectPlaced = false;
        planeManager.enabled = true;

        if (placedInstance != null)
        {
            Destroy(placedInstance);
            placedInstance = null;
        }
    }
}