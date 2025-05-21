using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneLoader : MonoBehaviour
{
    [SerializeField] private float delay = 3f; // Time before switching scene

    void Start()
    {
        StartCoroutine(LoadModelSelectionAfterDelay());
    }

    IEnumerator LoadModelSelectionAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Wait for set time
        SceneManager.LoadScene("ModelSelection"); // Load Model Selection scene
    }
}
