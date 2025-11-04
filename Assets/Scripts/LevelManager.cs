using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Optional UI")]
    public GameObject levelCompletePanel;  // assign in inspector if you have one

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CompleteLevel()
    {
        Debug.Log("Level Complete!");

        // show a panel if you have one
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        // OR load next scene automatically:
        // Scene current = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(current.buildIndex + 1);
    }
}