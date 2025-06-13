using Unity.VisualScripting;
using UnityEngine;

public class SigilManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject FireballPrefab;
    [SerializeField] private GameObject Player;
    private ISigil FireballSigil;

    
    void Start()
    {
        FireballSigil = new FireballSigil();
        Player.GetComponent<PlayerController>().PlayerUsedSigil += OnPlayerUsedSigil;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerUsedSigil(Vector2Int castPosition, GameObject caster)
    {
        if (FireballSigil != null)
        {
            FireballSigil.Activate(castPosition, caster, FireballPrefab);
        }
        else
        {
            Debug.LogWarning("FireballSigil is not initialized.");
        }
    }
}
