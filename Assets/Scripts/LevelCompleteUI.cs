using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelRoot;   // the overlay panel with "Level Complete" & Next button

    private void Start()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);  // hidden at start
    }

    public void Show()
    {
        if (panelRoot != null)
            panelRoot.SetActive(true);
    }

    public void OnNextLevel()
    {
        // load the next scene in build index
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex + 1);
    }

    // Optional: restart current level
    public void OnRetry()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}