using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameLoader
{
    public void LoadSceneByIndex(int index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        var allIndex = SceneManager.sceneCountInBuildSettings;
        var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < allIndex)
            SceneManager.LoadScene(nextIndex);
        else
            SceneManager.LoadScene(0);
    }

    public void ResetCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}