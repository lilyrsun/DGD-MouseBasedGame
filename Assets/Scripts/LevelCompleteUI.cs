using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelRoot;   // the overlay panel with "Level Complete" & Next button

    void Start()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);  // hidden at start
    }

    public void Show()
    {
        if (panelRoot != null)
            panelRoot.SetActive(true);

        // Optional: hard-freeze the mouse
        var mouse = FindObjectOfType<MouseController>();
        if (mouse != null)
        {
            var col = mouse.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
            mouse.enabled = false;
        }

        Time.timeScale = 0f;
    }

    public void OnNextLevel()
    {
        // 🔹 Unfreeze before loading
        Time.timeScale = 1f;

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex + 1);
    }

    public void OnRetry()
    {
        // 🔹 Unfreeze before reloading
        Time.timeScale = 1f;

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}