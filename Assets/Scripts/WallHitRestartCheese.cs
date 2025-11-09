using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WallHitRestartCheese : MonoBehaviour
{
    [SerializeField] string wallTag = "Wall";
    [SerializeField] AudioSource squeak;
    [SerializeField] AudioClip squeakClip;
    [SerializeField] float reloadDelay = 0.12f;

    bool reloading = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 🔹 If the game is paused (pre-start or post-finish), ignore collisions
        if (Time.timeScale == 0f) return;

        if (reloading) return;

        if (other.CompareTag(wallTag))
        {
            reloading = true;

            if (squeak && squeakClip)
                squeak.PlayOneShot(squeakClip);

            StartCoroutine(ReloadSoon());
        }
    }

    IEnumerator ReloadSoon()
    {
        // this uses scaled time, which is what we want while playing
        yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}