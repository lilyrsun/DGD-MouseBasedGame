using UnityEngine;

public class LevelEndMarker : MonoBehaviour
{
    [Header("References")]
    public LevelScroller[] levelScrollers;   // all scrollers to stop
    public LevelCompleteUI levelCompleteUI;  // UI controller to show overlay

    [Header("Trigger Settings")]
    public float extraDistanceBelowCamera = 2f; // how far past the camera before we trigger

    Transform cam;
    bool triggered = false;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (triggered || cam == null) return;

        // Map is moving DOWN. This marker is a child of the moving level root.
        // When its world Y is lower than (camera Y - extraDistance), we consider the level "passed".
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

            // Show "Level Complete" overlay
            if (levelCompleteUI != null)
                levelCompleteUI.Show();
        }
    }
}