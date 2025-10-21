using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }
}