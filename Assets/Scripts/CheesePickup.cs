using UnityEngine;

public class CheesePickup : MonoBehaviour
{
    public int value = 1;                 // how much this cheese is worth
    public AudioClip pickupClip;          // drag your cheese SFX here
    [Range(0f, 1f)] public float volume = 1f;

    bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        // Only react to the player mouse
        if (other.GetComponent<MouseController>() != null)
        {
            collected = true;

            // Play sound at camera position so it's always audible
            if (pickupClip != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(pickupClip, Camera.main.transform.position, volume);
            }

            // Tell the *collection* cheese manager we got some cheese
            if (CollectionCheeseManager.Instance != null)
            {
                CollectionCheeseManager.Instance.AddCheese(value);
            }
            else
            {
                Debug.LogWarning("No CollectionCheeseManager.Instance in scene!");
            }

            // Immediately remove the cheese from the scene
            Destroy(gameObject);
        }
    }
}