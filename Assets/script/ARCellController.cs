using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCellController : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private Vector2 touchStartPos;
    private float initialDistance;
    private Vector3 initialScale;
    private bool isPlaced = false;
    private Vector3 offset;
    private float objectHeight;

    void Start()
    {
        raycastManager = Object.FindFirstObjectByType<ARRaycastManager>();
        initialScale = transform.localScale;
        objectHeight = transform.position.y;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (isPlaced)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
                    {
                        Vector3 touchPosition = GetWorldPositionOnPlane(touch.position, objectHeight);
                        offset = transform.position - touchPosition;
                    }
                }
                else
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();
                    if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                    {
                        transform.position = hits[0].pose.position;
                        isPlaced = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isPlaced)
            {
                Vector3 touchPosition = GetWorldPositionOnPlane(touch.position, objectHeight);
                transform.position = touchPosition + offset;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch1.position, touch2.position);
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float scaleFactor = currentDistance / initialDistance;
                transform.localScale = initialScale * scaleFactor;
            }
        }
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
