using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f; // Health of the enemy
    public GameObject BattleGrid; // Reference to the BattleGrid GameObject
    public Vector2Int enemyPosition; 
    public float cooldown = 2f; // Cooldown for enemy movement
    private float lastMoveTime = 0f; // Time of the last move
    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<string, Vector2Int> moveOptions = new Dictionary<string, Vector2Int>()
    {
        { "Up", new Vector2Int(0, -1) }, 
        { "Left", new Vector2Int(-1, 0) }, 
        { "Down", new Vector2Int(0, 1) }, 
        { "Right", new Vector2Int(1, 0) },
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (BattleGrid == null)
        {
            Debug.LogError("BattleGrid reference is not set. Please assign it in the Inspector.");
            return;
        }
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
        MoveEnemy(enemyPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastMoveTime < cooldown) // Check if cooldown has passed
        {
            return;
        }

        List<Vector2Int> availableMoves = CheckAvailableMoves(); // Get available moves
        Debug.Log($"Available Moves: {availableMoves.Count}"); // Log the number of available moves
        if (availableMoves.Count > 0)
        {
            Vector2Int moveTo = availableMoves[RandomManager.Range(0, availableMoves.Count)]; // Randomly select a move
            MoveEnemy(moveTo); // Move enemy to the new position
            lastMoveTime = Time.time; // Update last move time
        }
    }
    private List<Vector2Int> CheckAvailableMoves()
    {
        List<Vector2Int> availableMoves = new List<Vector2Int>();

        foreach (var move in moveOptions)
        {
            Vector2Int newPosition = enemyPosition + move.Value;
            if (tiles.ContainsKey(newPosition) && 
                tiles[newPosition].GetComponent<TileController>().OccupiedBy == null &&
                tiles[newPosition].GetComponent<TileController>().tileType == "EnemyTile")
            {
                availableMoves.Add(newPosition);
            }
        }

        return availableMoves;
    }
    
    public void MoveEnemy(Vector2Int newPosition)
    {
        tiles[enemyPosition].GetComponent<TileController>().OccupiedBy = null; // Clear previous tile occupation
        enemyPosition = newPosition; // Update enemy position
        float newX = tiles[enemyPosition].transform.position.x;
        float newY = tiles[enemyPosition].transform.position.y;
        transform.position = new Vector3(newX, newY, -1); // Move enemy to new position
        tiles[enemyPosition].GetComponent<TileController>().OccupiedBy = gameObject; // Update tile occupation
    }
    public void TakeDamage(float damage)
    {
        // Implement enemy damage logic here
        Debug.Log($"Enemy at position {enemyPosition} took {damage} damage.");
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject); // Call the Die method if health is zero or below
        }
    }
}
