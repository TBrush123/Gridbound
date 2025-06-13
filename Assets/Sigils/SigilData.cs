using UnityEngine;

public class SigilData
{
    public string Name;
    public float Damage;
    public float Cooldown;
    public float CastTime;
    public float LastUsedTime; // Time when the sigil was last used
    public bool IsOnCooldown => (Time.time - LastUsedTime) < Cooldown; // Check if the sigil is on cooldown
}

