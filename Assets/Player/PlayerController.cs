using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveCooldown = 0.12f;
    public float moveTimer = 0f;
    public bool isMoving = false; // Flag to check if the player is moving
    public GameObject BattleGrid;
    public Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    public Vector2Int playerPosition = new Vector2Int(1, 1); // Starting position of the player
    private Dictionary<string, Vector2Int> moveOptions = new Dictionary<string, Vector2Int>()
    {
        { "W", new Vector2Int(0, -1) }, // Up
        { "A", new Vector2Int(-1, 0) }, // Left
        { "S", new Vector2Int(0, 1) }, // Down
        { "D", new Vector2Int(1, 0) } // Right
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < BattleGrid.transform.childCount; i++)
        {
            GameObject tile = BattleGrid.transform.GetChild(i).gameObject;
            TileController tileController = tile.GetComponent<TileController>();
            if (tileController != null)
            {
                Vector2Int position = tileController.position;
                tiles[position] = tile;
            }
        }
        StartCoroutine(MovePlayer(playerPosition));// Set initial player position
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(); // Call the Shoot method when the left mouse button is pressed
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CheckMoveAbility("A");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CheckMoveAbility("S");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CheckMoveAbility("D");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            CheckMoveAbility("W");
        }
         // Reset the timer after processing input
    }

    private bool CheckMoveAbility(string direction)
    {
        if (!moveOptions.ContainsKey(direction))
        {
            return false;           
        }
        Vector2Int newPosition = playerPosition + moveOptions[direction];
        Debug.Log(newPosition);
        if (!tiles.ContainsKey(newPosition))
        {
            Debug.Log("Invalid move: " + direction);
            return false; // Invalid move, tile does not exist
        }
        if (tiles[newPosition].GetComponent<TileController>().tileType != "PlayerTile")
        {
            Debug.Log("Tile is not yours: " + direction);
            return false; // Tile is not a player tile
        }
        StartCoroutine(MovePlayer(newPosition)); // Move player
        return true;
    }
    private void Shoot()
    {
        // Implement shooting logic here
        Debug.Log("Shoot action triggered!");
        // ToDo: Implement shooting logic here
    
    
    }
    public IEnumerator MovePlayer(Vector2Int newPosition)
    {
        if (isMoving) // Check if the player is already moving
        {
            yield break; // Exit if already moving
        }
        isMoving = true; // Set moving flag to true
        yield return new WaitForSeconds(moveCooldown);

        playerPosition = newPosition; // Update player position

        float newX = tiles[playerPosition].transform.position.x + 0.1f;
        float newY = tiles[playerPosition].transform.position.y + 1.4f;

        transform.position = new Vector3(newX, newY, -1); // Move player to new position
        tiles[playerPosition].GetComponent<TileController>().OccupiedBy = gameObject; // Update tile occupation

        yield return new WaitForSeconds(moveCooldown);
        isMoving = false; // Reset moving flag
    }
}


