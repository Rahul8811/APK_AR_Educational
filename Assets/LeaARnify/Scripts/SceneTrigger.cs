using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string nextSceneName = "HubScene"; // Name of your next scene

    void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object is the AR camera (user)
        if (other.CompareTag("MainCamera"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}