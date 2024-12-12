using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int maxLevel = 5;

    void Start()
    {
        StartLevel(currentLevel);
    }

    public void CompleteLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            StartLevel(currentLevel);
        }
        else
        {
            Debug.Log("Game Completed!");
            // Implement game completion logic here
        }
    }

    private void StartLevel(int level)
    {
        Debug.Log("Starting Level " + level);
        // Implement logic to set up the level based on difficulty
        // Increase maze complexity, enemy count, and traps based on level
    }
}
