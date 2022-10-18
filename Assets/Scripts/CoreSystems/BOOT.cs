using UnityEngine;
using UnityEngine.SceneManagement;

// Every scene should have this script on
public class BOOT : MonoBehaviour
{
    private string bootSceneName = "BOOT";
    private string defaultStartingSceneName = "MainMenu";
    private string currentActiveSceneName { get => SceneManager.GetActiveScene().name; }

    private void Awake()
    {
        SceneStartup();
    }

    private void SceneStartup()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log(this + ": GameManager is missing in scene " + currentActiveSceneName + ", creating a new one.");
            CreateGameManager();
        }

        if (currentActiveSceneName == bootSceneName && GameManager.Instance != null)
        {
            Debug.Log(this + ": GameManager.Instance is present. Loading default starting scene: "
                + defaultStartingSceneName + "...");
            LoadDefaultStartingScene();
        }
    }

    private void LoadDefaultStartingScene()
    {
        SceneManager.LoadScene(defaultStartingSceneName);
    }

    private void CreateGameManager()
    {
        GameObject gameManager = new GameObject("[GameManager]");
        gameManager.AddComponent<GameManager>();
    }
}
