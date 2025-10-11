using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class MouseSqueeze : MonoBehaviour
{
    [SerializeField] float scaleFactor = 0.65f;
    [SerializeField] float duration = 0.35f;
    [SerializeField] float cooldown = 0.8f;
    bool available = true;
    Vector3 baseScale;

    void Awake() => baseScale = transform.localScale;

    void Update()
    {
        if (available && Input.GetMouseButtonDown(0))
            StartCoroutine(Squeeze());
    }

    IEnumerator Squeeze()
    {
        available = false;
        transform.localScale = baseScale * scaleFactor;
        yield return new WaitForSeconds(duration);
        transform.localScale = baseScale;
        yield return new WaitForSeconds(cooldown);
        available = true;
    }
}
