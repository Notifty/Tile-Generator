using UnityEngine;
using UnityEngine.UI;

public class KeyCounter : MonoBehaviour
{
    public Text keyCounterText; // Assign this in the inspector

 
    public void UpdateKeyCounter(int keysCollected, int totalKeys)
    {
        if (keyCounterText != null)
        {
            keyCounterText.text = "Keys Collected: " + keysCollected + "/" + totalKeys;
        }
        else
        {
            Debug.LogWarning("KeyCounterText is not assigned in the inspector.");
        }
    }
}
