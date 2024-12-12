using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Key Management")]
    public int totalKeys = 3; // Total number of keys to collect
    private int keysCollected = 0;

    [Header("Player Management")]
    public Transform playerStartPosition; // Assign this to the start position in the inspector
    public GameObject playerPrefab; // Reference to the player prefab
    public GameObject playerInstance; // Reference to the actual player instance in the scene

    [Header("UI Management")]
    public KeyCounter keyCounter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (keyCounter != null && keyCounter.keyCounterText != null)
        {
            keyCounter.keyCounterText.text = "Test Text: 0/3"; // Directly set some text
        }
        InitializePlayer();
        UpdateKeyCounterUI(); // Update UI at the start of the game
    }

    public void AddKey()
    {
        keysCollected++;
        UpdateKeyCounterUI(); // Update the UI each time a key is collected
    }

    public bool HasCollectedAllKeys()
    {
        return keysCollected >= totalKeys;
    }

    public void ResetKeys()
    {
        keysCollected = 0;
        UpdateKeyCounterUI(); // Reset the UI when keys are reset
    }

    public void ResetPlayerPosition()
    {
        if (playerInstance != null && playerStartPosition != null)
        {
            playerInstance.transform.position = playerStartPosition.position;
        }
        
    }

    public GameObject GetPlayer()
    {
        return playerInstance;
    }

    public void InitializePlayer()
    {
        if (playerPrefab != null && playerStartPosition != null)
        {
            if (playerInstance == null)
            {
                playerInstance = Instantiate(playerPrefab, playerStartPosition.position, Quaternion.identity);
            }
            else
            {
                playerInstance.transform.position = playerStartPosition.position;
            }
        }
        else
        {
            Debug.LogWarning("Player prefab or start position is not assigned.");
        }
    }

    private void UpdateKeyCounterUI()
    {
        if (keyCounter != null)
        {
            keyCounter.UpdateKeyCounter(keysCollected, totalKeys);
        }
        else
        {
            Debug.LogWarning("KeyCounter script is not assigned in the inspector.");
        }
    }
}
