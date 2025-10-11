using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MouseController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float followLerp = 25f;  // higher = snappier
    [SerializeField] Vector2 worldBoundsMin = new(-10f, -5f);
    [SerializeField] Vector2 worldBoundsMax = new(10f, 5f);

    void Reset() { if (!cam) cam = Camera.main; }

    void Update()
    {
        Vector3 m = Input.mousePosition;
        m.z = Mathf.Abs(cam.transform.position.z);
        Vector3 target = cam.ScreenToWorldPoint(m);
        target.z = 0f;
        // Keep player in camera view even if walls scroll.
        target.x = Mathf.Clamp(target.x, worldBoundsMin.x, worldBoundsMax.x);
        target.y = Mathf.Clamp(target.y, worldBoundsMin.y, worldBoundsMax.y);

        transform.position = Vector3.Lerp(transform.position, target, 1f - Mathf.Exp(-followLerp * Time.deltaTime));
    }
}
