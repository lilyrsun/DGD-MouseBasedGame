using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string firstLevelSceneName = "Level1";  // set this in inspector

    public void StartGame()
    {
        Time.timeScale = 1f;   // just in case coming from a paused scene
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}