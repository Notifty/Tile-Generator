using UnityEngine;

public class MazeObjectPlacer : MonoBehaviour
{
    public TileMazeGenerator mazeGenerator;
    public GameObject[] objectsToPlace; // Array of objects (keys, power-ups, enemies) to place in the maze
    public int numberOfKeys = 3; // Number of keys to place in the maze
    public int numberOfPowerUps = 2; // Number of power-ups to place in the maze
    public int numberOfEnemies = 2; // Number of enemies to place in the maze

    void Start()
    {
        PlaceObjectsInMaze();
    }

    void PlaceObjectsInMaze()
    {
        if (mazeGenerator == null || objectsToPlace.Length == 0)
        {
            Debug.LogError("Maze generator or objects to place are not assigned.");
            return;
        }

        int[,] maze = mazeGenerator.maze; // Access the maze

        PlaceSpecificObject(objectsToPlace[0], numberOfKeys); // Assuming objectsToPlace[0] is a key prefab
        PlaceSpecificObject(objectsToPlace[1], numberOfPowerUps); // Assuming objectsToPlace[1] is a power-up prefab
        PlaceSpecificObject(objectsToPlace[2], numberOfEnemies); // Assuming objectsToPlace[2] is an enemy prefab
    }

    void PlaceSpecificObject(GameObject objectToPlace, int count)
    {
        int[,] maze = mazeGenerator.maze;

        for (int i = 0; i < count; i++)
        {
            Vector2Int randomPosition = GetRandomOpenPosition(maze);
            Vector3 position = new Vector3(randomPosition.x, randomPosition.y, 0);
            Instantiate(objectToPlace, position, Quaternion.identity);
        }
    }

    Vector2Int GetRandomOpenPosition(int[,] maze)
    {
        int width = maze.GetLength(0);
        int height = maze.GetLength(1);
        Vector2Int position;

        do
        {
            position = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (maze[position.x, position.y] != 0); // Ensure the position is on an open tile

        return position;
    }
}
