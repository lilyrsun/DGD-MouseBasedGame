using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 2f;

    [Header("Control")]
    public bool startActive = false;   // can leave this false so it waits for Start Pad
    bool isActive;

    void Awake()
    {
        isActive = startActive;
    }

    void Update()
    {
        if (!isActive) return;
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}