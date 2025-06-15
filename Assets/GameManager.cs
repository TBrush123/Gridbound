using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int seed = 0;
    private Vector2 tileSize = new Vector2(33 / 15, 19 / 15);
    private void Awake()
    {
        RandomManager.Init(seed); // Initialize RandomManager with a default seed of 0
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Vector3 TileToWorldPosition(Vector2Int tilePosition)
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = tilePosition.x * tileSize.x - 8f;
        newPosition.y = -tilePosition.y * tileSize.y;
        newPosition.z = -0.1f * tilePosition.y;

        return newPosition;
    }
}
