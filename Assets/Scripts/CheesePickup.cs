using UnityEngine;

public class CheesePickup : MonoBehaviour
{
    public int value = 1;           // how much this cheese is worth
    public AudioSource pickupSound; // optional

    bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.GetComponent<MouseController>() != null)
        {
            collected = true;

            if (pickupSound != null)
                pickupSound.Play();

            // Tell the *collection* cheese manager we got some cheese
            if (CollectionCheeseManager.Instance != null)
            {
                CollectionCheeseManager.Instance.AddCheese(value);
            }
            else
            {
                Debug.LogWarning("No CollectionCheeseManager.Instance in scene!");
            }

            Destroy(gameObject);
        }
    }

}