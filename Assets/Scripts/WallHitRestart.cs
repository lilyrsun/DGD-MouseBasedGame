using UnityEngine;
using UnityEngine.SceneManagement;

public class WallHitRestart : MonoBehaviour
{
    [SerializeField] string wallTag = "Wall";
    [SerializeField] AudioSource squeak;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(wallTag))
        {
            if (squeak) squeak.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
