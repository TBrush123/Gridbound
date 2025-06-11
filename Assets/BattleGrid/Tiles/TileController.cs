using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public GameObject OccupiedBy = null;
    public Vector2Int position;
    public string tileType;
    private Dictionary<string, Sprite> tilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilePrefab = new Dictionary<string, Sprite>
        {
            { "PlayerTile", Resources.Load<Sprite>("Tile/TilePlayer") },
            { "EnemyTile", Resources.Load<Sprite>("Tile/TileEnemy") },
        };
        tileType = (position.x <= 2) ? "PlayerTile" : "EnemyTile"; // Default tile type, can be changed later
        SetTileType(tileType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 public void SetTileType(string type)
    {
        tileType = type;
        if (tilePrefab.ContainsKey(tileType))
        {
            GetComponent<SpriteRenderer>().sprite = tilePrefab[tileType];
        }
        else
        {
            Debug.LogWarning($"Tile type '{tileType}' not found in tilePrefab dictionary.");
        }
    }
}
