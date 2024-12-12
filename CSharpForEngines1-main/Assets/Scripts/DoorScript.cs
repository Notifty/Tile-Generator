using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public AudioClip doorOpenSound; // Sound to play when the door opens

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.HasCollectedAllKeys())
            {
                // Play the door opening sound
                if (doorOpenSound != null)
                {
                    AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
                }

                // Optionally: Reset key collection status
                GameManager.instance.ResetKeys();

                // Reload the "Level1" scene
                SceneManager.LoadScene("Two");
            }
        }
    }
}
