using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectionLevelEndMarker : MonoBehaviour
{
    [Header("References")]
    public LevelScroller[] levelScrollers;              // all scrollers to stop
    public CollectionLevelCompleteUI levelCompleteUI;   // UI controller to show overlay

    [Header("Final Level Settings")]
    [Tooltip("If true, and all cheese is collected when end is reached, load victorySceneName.")]
    public bool isFinalLevel = false;
    public string victorySceneName = "Victory";   // set this to your end screen scene name

    [Header("Trigger Settings")]
    public float extraDistanceBelowCamera = 2f;

    Transform cam;
    bool triggered = false;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (triggered || cam == null) return;

        float thresholdY = cam.position.y - extraDistanceBelowCamera;

        if (transform.position.y <= thresholdY)
        {
            triggered = true;

            // Stop all scrolling
            foreach (var scroller in levelScrollers)
            {
                if (scroller != null)
                    scroller.SetActive(false);
            }

            bool hasAllCheese = false;
            if (CollectionCheeseManager.Instance != null)
                hasAllCheese = CollectionCheeseManager.Instance.AllCheeseCollected();

            // FINAL LEVEL + all cheese -> go straight to victory scene
            if (isFinalLevel && hasAllCheese)
            {
                Time.timeScale = 1f; // make sure game isn't paused
                SceneManager.LoadScene(victorySceneName);
            }
            else
            {
                // Otherwise, show the panel ("Still Hungry..." or "Level Complete!")
                if (levelCompleteUI != null)
                    levelCompleteUI.Show();
            }
        }
    }
}