using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Interaction Manager Initialized");

        // Ensure all child objects have ObjectTouchHandler
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ObjectTouchHandler>() == null)
            {
                child.gameObject.AddComponent<ObjectTouchHandler>();
            }
        }
    }
}
