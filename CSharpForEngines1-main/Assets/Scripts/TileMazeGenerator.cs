using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMazeGenerator : MonoBehaviour
{
    [Header("Maze Settings")]
    public int width = 20;
    public int height = 20;
    public Tilemap wallTilemap;
    public Tilemap floorTilemap;
    public TileBase wallTile;
    public TileBase floorTile;
    public Vector2Int startPosition = new Vector2Int(1, 1); // Player start position
    public GameObject playerPrefab; // Reference to the player prefab

    [Header("Door Settings")]
    public GameObject doorPrefab; // Reference to the door prefab

    public int[,] maze { get; private set; } // Accessible maze array
    private bool playerPlaced = false;

    void Start()
    {
        GenerateMaze();
        DrawMaze();
        PlacePlayer(); // Only call PlacePlayer() once after the maze is generated
        PlaceDoor(); // Place the door at the maze exit
    }

    public void GenerateMaze()
    {
        maze = new int[width, height];

        // Initialize maze with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // 1 means wall
            }
        }

        // Carve a single path using Recursive Backtracking algorithm
        CarveMaze(startPosition.x, startPosition.y);

        // Ensure the start and exit positions are open
        maze[startPosition.x, startPosition.y] = 0;
        for (int x = width - 3; x < width - 1; x++)
        {
            for (int y = height - 3; y < height - 1; y++)
            {
                maze[x, y] = 0; // Set a larger exit area
            }
        }
    }

    void CarveMaze(int x, int y)
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        directions = ShuffleArray(directions);

        foreach (Vector2Int dir in directions)
        {
            int nx = x + dir.x * 2;
            int ny = y + dir.y * 2;

            if (IsInBounds(nx, ny) && maze[nx, ny] == 1)
            {
                maze[x + dir.x, y + dir.y] = 0; // Carve the path between cells
                maze[nx, ny] = 0; // Carve the next cell
                CarveMaze(nx, ny);
            }
        }
    }

    bool IsInBounds(int x, int y)
    {
        return x > 0 && x < width - 1 && y > 0 && y < height - 1;
    }

    Vector2Int[] ShuffleArray(Vector2Int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector2Int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        return array;
    }

    public void DrawMaze()
    {
        wallTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (maze[x, y] == 1)
                {
                    wallTilemap.SetTile(position, wallTile);
                }
                else
                {
                    floorTilemap.SetTile(position, floorTile);
                }
            }
        }
    }

    public void PlacePlayer()
    {
        if (!playerPlaced)
        {
            Vector3 playerPosition = new Vector3(startPosition.x, startPosition.y, 0);
            GameObject player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            GameManager.instance.playerInstance = player; // Update the GameManager with the player instance
            playerPlaced = true; // Ensure the player is only placed once
        }
    }

    public void PlaceDoor()
    {
        if (doorPrefab != null)
        {
            // Assuming the exit is in the bottom-right corner of the maze
            Vector3 doorPosition = new Vector3(width - 2, height - 2, 0); // Adjust position as needed
            Instantiate(doorPrefab, doorPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Door prefab is not assigned in the inspector.");
        }
    }
}
