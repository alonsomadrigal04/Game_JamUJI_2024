using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;

    // ------ PROBABILITY ------
    private int probabilitySmall = 10;
    private int probabilityMedium = 7;
    private int probabilityLarge = 5;
    private int probabilityNumber;
    private int probabilitySpawn;

    // ------ OBJECTS ------
    public GameObject smallObject;
    public GameObject mediumObject;
    public GameObject largeObject;

    // ------ TIMER ------
    public float timer;
    public int durationTimer;

    // ------ QUANTITY ------
    public int quantityObjects = 0;
    public int maxQuatityObjects = 5;

    Collider2D collider2D;

    [SerializeField] private LayerMask _layerObjectCantSpawnOn;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        probabilityNumber = probabilitySmall + probabilityMedium + probabilityLarge;
    }

    private void Update()
    {
        Timer();
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer > durationTimer && quantityObjects < maxQuatityObjects)
        {
            probabilitySpawn = UnityEngine.Random.Range(1, probabilityNumber);
            if (probabilitySpawn <= probabilitySmall)
            {
                instance.SpawnPickableObjects(collider2D, smallObject);
            }
            else if (probabilitySpawn < probabilitySmall + probabilityMedium + 1)
            {
                instance.SpawnPickableObjects(collider2D, mediumObject);
            }
            else
            {
                instance.SpawnPickableObjects(collider2D, largeObject);
            }
            quantityObjects++;
            timer = 0;
        }
    }
    public void SpawnPickableObjects(Collider2D spawnableAreaCol, GameObject pickableObjects)
    {
        
            Vector2 spawnPositon = RandomSpawnPosition(spawnableAreaCol);
            GameObject spawnedObject = Instantiate(pickableObjects, spawnPositon, Quaternion.identity);
        
    }

    private Vector2 RandomSpawnPosition(Collider2D spawnableAreaCol)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool spawnPosOk = false;

        int attempCount = 0;
        int maxAttemps = 200;

        int layerToNotSpawnOn = LayerMask.NameToLayer("");

        while(!spawnPosOk && attempCount < maxAttemps)
        {
            spawnPosition = RandomPointInCollider(spawnableAreaCol);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.25f);

            bool isInvalidCollision = false;
            foreach(Collider2D collider in colliders)
            {
                if(((1 << collider.gameObject.layer) & _layerObjectCantSpawnOn)!= 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }


            if(!isInvalidCollision)
            {
                spawnPosOk = true;
            }

            attempCount++;
        }

        if(!spawnPosOk)
        {
            Debug.LogWarning("No hay lugar chacho");
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
