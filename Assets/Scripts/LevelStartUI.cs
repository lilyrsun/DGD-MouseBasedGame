using UnityEngine;

public class LevelStartUI : MonoBehaviour
{
    [Header("Scrollers to activate")]
    public LevelScroller[] levelScrollers;

    [Header("UI")]
    public GameObject startButtonRoot;   // assign the button or a panel that holds it

    public void StartLevel()
    {
        // turn on all the scrollers
        foreach (var s in levelScrollers)
        {
            if (s != null) s.SetActive(true);
        }

        // hide the UI button after click
        if (startButtonRoot != null)
            startButtonRoot.SetActive(false);
    }
}