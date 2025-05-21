using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Function to load a specific scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Function to go back to the Model Selection screen
    public void LoadModelSelection()
    {
        SceneManager.LoadScene("ModelSelection");
    }

    // Function to go back to the Home screen
    public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }

    // Function to quit the application
    public void QuitApplication()
    {
        Application.Quit();
    }
}
