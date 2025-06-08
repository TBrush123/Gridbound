using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int seed = 0;
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
}
