using System;

public static class RandomManager
{
    private static System.Random rng;
    private static int currentSeed;

    public static void Init(int seed)
    {
        rng = new System.Random(seed);
        currentSeed = seed;
        UnityEngine.Debug.Log($"[RandomManager] Seed initialized: {seed}");
    }

    public static void Seed(int newSeed)
    {
        Init(newSeed);
        UnityEngine.Debug.Log($"[RandomManager] Seed changed to: {newSeed}");
    }

    public static int GetCurrentSeed()
    {
        return currentSeed;
    }

    public static int Range(int min, int max)
    {
        EnsureInitialized();
        return rng.Next(min, max);
    }

    public static float Range01()
    {
        EnsureInitialized();
        return (float)rng.NextDouble();
    }

    public static bool Bool()
    {
        EnsureInitialized();
        return rng.NextDouble() < 0.5;
    }

    private static void EnsureInitialized()
    {
        if (rng == null)
        {
            UnityEngine.Debug.LogWarning("[RandomManager] RNG not initialized, using default seed 0.");
            Init(0);
        }
    }
}
