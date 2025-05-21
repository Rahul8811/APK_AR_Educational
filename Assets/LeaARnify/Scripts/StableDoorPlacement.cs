using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class StableDoorPlacement : MonoBehaviour
{
    [Header("Door Settings")]
    [Tooltip("The door prefab to place")]
    public GameObject doorPrefab;

    [Tooltip("Real-world height of the door in meters (e.g., 2.5m)")]
    public float doorHeight = 2.5f;

    [Tooltip("Offset from detected plane surface")]
    public float groundOffset = 0.01f;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private GameObject placedDoor;
    private bool placementLocked = false;
    private ARPlane lockedPlane;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        if (placementLocked || doorPrefab == null) return;

        // Touch input for placement
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var touch = Input.GetTouch(0);
            PlaceDoor(touch.position);
        }
    }

    void PlaceDoor(Vector2 screenPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            lockedPlane = hits[0].trackable as ARPlane;

            // Create or reposition door
            if (placedDoor == null)
            {
                placedDoor = Instantiate(doorPrefab, hitPose.position, Quaternion.identity);

                // Scale to real-world size
                float scaleFactor = doorHeight / GetObjectHeight(placedDoor);
                placedDoor.transform.localScale = Vector3.one * scaleFactor;

                // Add AR anchor
                placedDoor.AddComponent<ARAnchor>();
            }
            else
            {
                placedDoor.transform.position = hitPose.position;
            }

            // Align with plane and camera
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; // Keep upright
            placedDoor.transform.rotation = Quaternion.LookRotation(cameraForward);

            // Adjust position to sit on plane
            placedDoor.transform.position = new Vector3(
                hitPose.position.x,
                hitPose.position.y + (placedDoor.transform.localScale.y / 2) + groundOffset,
                hitPose.position.z
            );

            placementLocked = true;
            DisablePlaneVisualization();
        }
    }

    float GetObjectHeight(GameObject obj)
    {
        Renderer rend = obj.GetComponentInChildren<Renderer>();
        return rend != null ? rend.bounds.size.y : 1f;
    }

    void DisablePlaneVisualization()
    {
        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }

    // Call this to reset placement
    public void UnlockPlacement()
    {
        placementLocked = false;
        planeManager.enabled = true;
    }
}