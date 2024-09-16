using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static void Restart()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneID);
    }

    public static void LoadNextScene()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;
        currentSceneID++;
        SceneManager.LoadScene(currentSceneID);
    }

    public static void Exit()
    {
        Application.Quit();
    }
}
