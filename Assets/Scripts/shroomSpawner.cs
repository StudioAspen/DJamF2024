using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSpawner : MonoBehaviour
{
    PlayerManager player;

    [Header("Spawn Area")]
    [SerializeField] private LayerMask layersShroomCannotSpawnOn;
    [SerializeField] private Collider2D roomspawnarea;

    [Header("Spawn Behaviour")]
    [SerializeField] private GameObject[] shrooms;
    [SerializeField] private Vector2 spawnIntervalRange = new Vector2(3f, 5f);
    public float SpawnIntervalMultiplier = 1f;
    private float spawnInterval;
    private float spawnShroomsTimer;
    public bool CanSpawn = true;

    [SerializeField] private int maxShrooms = 100;
    public int ShroomCount;
    private List<GameObject> shroomObjects = new List<GameObject>();
    [SerializeField] private float drainStrength = 10f;


    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
    }

    private void Update()
    {
        PeriodicSpawningUpdate();

        float percentFull = ShroomCount / (float)maxShrooms;

        player.CurrentHealth -= drainStrength * percentFull * Time.deltaTime;
    }

    private void PeriodicSpawningUpdate()
    {
        if (!CanSpawn) return;
        if (ShroomCount >= maxShrooms) return;

        spawnShroomsTimer += Time.deltaTime;

        if(spawnShroomsTimer > spawnInterval)
        {
            spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            spawnInterval *= SpawnIntervalMultiplier;

            spawnShroomsTimer = 0;

            shroomObjects.Add(SpawnShrooms(roomspawnarea, shrooms));
            ShroomCount++;
        }
    }

    public void KillAllShrooms()
    {
        for(int i = 0; i < shroomObjects.Count; i++)
        {
            Destroy(shroomObjects[i]);
        }

        ShroomCount = 0;
        shroomObjects = new List<GameObject>();
    }

    public bool IsFull()
    {
        return ShroomCount >= maxShrooms;
    }
    public GameObject SpawnShrooms(Collider2D spawnableAreaCollider, GameObject[] shrooms)
    {
        Vector2 spawnPosition = RandomSpawnPosition(spawnableAreaCollider);
        GameObject spawnedShroom = Instantiate(shrooms[0], spawnPosition, Quaternion.identity);

        return spawnedShroom;
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
