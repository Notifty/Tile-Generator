using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public AudioClip pickupSound; // Assign a sound for key pickup
    private bool isCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            CollectKey();
        }
    }

    private void CollectKey()
    {
        isCollected = true;

        // Play the pickup sound
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        else
        {
            Debug.LogWarning("Pickup sound not assigned in the inspector.");
        }

        // Notify GameManager to add a key
        GameManager.instance.AddKey();

        // Destroy the key game object
        Destroy(gameObject);
    }
}
