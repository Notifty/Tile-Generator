using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleDoorScript : MonoBehaviour
{
    public string sceneName = "Level1"; // Ensure this matches your scene name

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReloadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        // Log the scene name to ensure it is correct
        Debug.Log("Reloading scene: " + sceneName);

        if (SceneManager.GetSceneByName(sceneName).IsValid())
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " is not valid or not found.");
        }
    }
}
