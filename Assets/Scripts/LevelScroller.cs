using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    [SerializeField] float baseSpeed = 2.5f;
    [SerializeField] float speedRampPerSec = 0.15f; // difficulty over time
    float currentSpeed;

    void OnEnable() => currentSpeed = baseSpeed;

    void Update()
    {
        currentSpeed += speedRampPerSec * Time.deltaTime;
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    public void ResetScroll()
    {
        currentSpeed = baseSpeed;
        // Optionally reset LevelRoot to start position if you don't reload scene.
    }
}
