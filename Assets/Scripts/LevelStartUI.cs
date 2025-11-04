using UnityEngine;

public class LevelStartUI : MonoBehaviour
{
    [Header("Scrollers to activate")]
    public LevelScroller[] levelScrollers;
    // public SegmentedLevelScroller[] segmentedScrollers;

    [Header("UI")]
    public GameObject startButtonRoot;

    public void StartLevel()
    {
        Debug.Log("StartLevel() CALLED");  // <--- add this line

        foreach (var s in levelScrollers)
        {
            if (s != null)
            {
                Debug.Log("Enabling LevelScroller on " + s.gameObject.name);
                s.SetActive(true);
            }
        }

        // foreach (var s in segmentedScrollers)
        // {
        //     if (s != null)
        //     {
        //         Debug.Log("Enabling SegmentedLevelScroller on " + s.gameObject.name);
        //         s.SetActive(true);
        //     }
        // }

        if (startButtonRoot != null)
            startButtonRoot.SetActive(false);
    }
}