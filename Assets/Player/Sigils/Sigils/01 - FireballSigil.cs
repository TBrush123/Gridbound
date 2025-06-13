using UnityEngine;

public class FireballSigil : ISigil
{
    public SigilData Data { get; private set; }

    public FireballSigil()
    {
        Data = new SigilData
        {
            Name = "Fireball",
            Damage = 25f,
            Cooldown = 1.5f,
            Range = 10f,
            CastTime = 0.3f
        };
    }

    public void Activate(Vector2Int castPosition, GameObject caster)
    {
        // Logic to activate the fireball effect
        Debug.Log($"Fireball activated at {castPosition} by {caster.name} with damage {Data.Damage}");

        //TODO: Implement actual fireball effect logic here
    }
}
