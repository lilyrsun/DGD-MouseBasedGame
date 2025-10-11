using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MouseController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float followLerp = 25f;  // higher = snappier
    [SerializeField] float boundsPadding = 0.2f; // keep mouse slightly inside view

    Vector2 minW, maxW;

    void Awake()
    {
        if (!cam) cam = Camera.main;
        RecalcBounds();
    }

    void OnValidate()
    {
        if (!cam) cam = Camera.main;
        if (cam) RecalcBounds();
    }

    void RecalcBounds()
    {
        // Viewport to world corners at z=0 plane
        Vector3 bl = cam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z)));
        Vector3 tr = cam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(cam.transform.position.z)));
        minW = new Vector2(bl.x + boundsPadding, bl.y + boundsPadding);
        maxW = new Vector2(tr.x - boundsPadding, tr.y - boundsPadding);
    }

    void Update()
    {
        Vector3 m = Input.mousePosition;
        m.z = Mathf.Abs(cam.transform.position.z);
        Vector3 target = cam.ScreenToWorldPoint(m);
        target.z = 0f;

        target.x = Mathf.Clamp(target.x, minW.x, maxW.x);
        target.y = Mathf.Clamp(target.y, minW.y, maxW.y);

        transform.position = Vector3.Lerp(transform.position, target, 1f - Mathf.Exp(-followLerp * Time.deltaTime));
    }
}
