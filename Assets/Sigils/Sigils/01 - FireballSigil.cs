using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
//This is a Megaman battle network inspired game, so the sigils are like chips in the game
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
            CastTime = 0.3f,
            LastUsedTime = 0f
        };
    }

    public void Activate(Vector2Int castPosition, GameObject caster, GameObject prefab)
    {
        Debug.Log($"Fireball activated at {castPosition} by {caster.name} with damage {Data.Damage}");

        GameObject instance = GameObject.Instantiate(prefab, caster.transform.position, Quaternion.identity);
        instance.transform.position = new Vector3(castPosition.x, castPosition.y, -1);

        // Start coroutine to move fireball
        caster.GetComponent<MonoBehaviour>().StartCoroutine(MoveFireball(instance, castPosition));
    }

    private IEnumerator MoveFireball(GameObject instance, Vector2Int startPos)
    {
        float moveDelay = 0.1f; // Delay between each cell
        int y = startPos.y;

        for (int x = startPos.x + 1; x < 8; x++)
        {
            instance.transform.position = new Vector3(x, y, -1);
            Debug.Log($"Fireball moving to {instance.transform.position}");
            yield return new WaitForSeconds(moveDelay);
        }
    }

}
