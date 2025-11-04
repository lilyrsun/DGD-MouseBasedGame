using UnityEngine;

public class LevelStartUI : MonoBehaviour
{
    [Header("Scrollers to activate")]
    public LevelScroller[] levelScrollers;

    [Header("UI")]
    public GameObject startButtonRoot;

    MouseController mouse;
    Collider2D mouseCollider;

    void Awake()
    {
        mouse = FindObjectOfType<MouseController>();
        if (mouse != null)
            mouseCollider = mouse.GetComponent<Collider2D>();
    }

    void Start()
    {
        // freeze time
        Time.timeScale = 0f;

        // disable mouse movement + collisions until we hit Start
        if (mouse != null)
            mouse.enabled = false;
        if (mouseCollider != null)
            mouseCollider.enabled = false;

        if (startButtonRoot != null)
            startButtonRoot.SetActive(true);
    }

    public void StartLevel()
    {
        Debug.Log("StartLevel() CALLED");

        Time.timeScale = 1f;

        foreach (var s in levelScrollers)
        {
            if (s != null)
            {
                Debug.Log("Enabling LevelScroller on " + s.gameObject.name);
                s.SetActive(true);
            }
        }

        if (mouse != null)
            mouse.enabled = true;
        if (mouseCollider != null)
            mouseCollider.enabled = true;

        if (startButtonRoot != null)
            startButtonRoot.SetActive(false);
    }
}