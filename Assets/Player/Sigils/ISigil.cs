using UnityEngine;

public interface ISigil
{
    SigilData Data { get; } // Reference to the SigilData class
    void Activate(Vector2Int castPosition, GameObject caster);
}
