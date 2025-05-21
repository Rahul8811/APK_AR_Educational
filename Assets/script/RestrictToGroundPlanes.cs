using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RestrictToGroundPlanes : MonoBehaviour
{
    private ARPlaneManager planeManager;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += FilterPlanes; // Run function when new planes are detected
    }

    void FilterPlanes(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            // Disable planes that are not flat ground
            if (plane.alignment != PlaneAlignment.HorizontalUp)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }
}
