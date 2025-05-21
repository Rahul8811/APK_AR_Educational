using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectDragHandler : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        // Updated to use the new method
        raycastManager = Object.FindFirstObjectByType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 1) // One finger touch for drag
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchingObject(touch))
                    {
                        isDragging = true;
                        offset = transform.position - GetTouchWorldPosition(touch);
                        Debug.Log("Object Selected: " + gameObject.name);
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        MoveObject(touch);
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    Debug.Log("Drag Ended: " + gameObject.name);
                    break;
            }
        }
    }

    // Check if touch is on this object
    bool IsTouchingObject(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform; // Ensure correct object
        }
        return false;
    }

    // Convert touch position to world space
    Vector3 GetTouchWorldPosition(Touch touch)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
        {
            return hits[0].pose.position;
        }
        return transform.position;
    }

    // Move the object to the touch position
    void MoveObject(Touch touch)
    {
        Vector3 newPosition = GetTouchWorldPosition(touch) + offset;
        transform.position = newPosition;
        Debug.Log("Moving Object: " + gameObject.name + " to " + newPosition);
    }
}
