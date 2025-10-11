using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WallHitRestart : MonoBehaviour
{
    [SerializeField] string wallTag = "Wall";
    [SerializeField] AudioSource squeak;
    [SerializeField] AudioClip squeakClip;   // drop a squeak clip here
    [SerializeField] float reloadDelay = 0.12f;

    bool reloading = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!reloading && other.CompareTag(wallTag))
        {
            reloading = true;
            if (squeak && squeakClip) squeak.PlayOneShot(squeakClip);
            StartCoroutine(ReloadSoon());
        }
    }

    IEnumerator ReloadSoon()
    {
        yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
