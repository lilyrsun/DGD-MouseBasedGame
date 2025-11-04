using UnityEngine;

public class CheesePickup : MonoBehaviour
{
    public int value = 1;           // how much this cheese is worth
    public AudioSource pickupSound; // optional

    bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        // Only react to the player mouse
        if (other.GetComponent<MouseController>() != null)
        {
            collected = true;

            if (pickupSound != null)
                pickupSound.Play();

            // Tell the manager we got some cheese
            CheeseManager.Instance.AddCheese(value);

            // If we used a sound on this object, you could delay destroy with a coroutine,
            // but simplest is just:
            Destroy(gameObject);
        }
    }
}