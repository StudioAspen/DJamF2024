using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask layersShroomCannotSpawnOn;
    public static ShroomSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnShrooms(Collider2D spawnableAreaCollider, GameObject[] shrooms)
    {
        foreach (GameObject shroom in shrooms)
        {
            Vector2 spawnPosition = RandomSpawnPosition(spawnableAreaCollider);
            GameObject spawnedShroom = Instantiate(shroom, spawnPosition, Quaternion.identity);
        }
    }
    
    private Vector2 RandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool isSpawnPosValid = false;

        int attemptCount = 0;
        int maxAttempts = 200;


        while (!isSpawnPosValid && attemptCount < maxAttempts)
        {
            spawnPosition = RandomPointInCollider(spawnableAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);

            bool isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                if (((1 << collider.gameObject.layer) & layersShroomCannotSpawnOn) !=0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPosValid = true;
            }

            attemptCount++;
        }

        if (!isSpawnPosValid)
        {
            Debug.LogWarning("Could not find valid spawn");
        }

    return spawnPosition;
    }

    private Vector2 RandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        Bounds collBounds = collider.bounds;
        Vector2 minBounds = new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY); 
    }

}
