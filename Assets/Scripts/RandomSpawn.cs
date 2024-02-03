using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // ------ PROBABILITY ------
    private int probabilitySmall = 10;
    private int probabilityMedium = 7;
    private int probabilityLarge = 5;
    private int probabilityNumber;
    private int probabilitySpawn;

    // ------ QUANTITY ------
    [HideInInspector]
    public int quantityObjects = 0;
    [HideInInspector]
    public int maxQuatityObjects = 5;

    // ------ OBJECTS ------
    public GameObject smallObject;
    public GameObject mediumObject;
    public GameObject largeObject;

    // ------ SPAWN ------
    private Vector2 randomPosition;
    private Vector2 spawnPostion;
    private int durationTimer = 3;
    private float timer;
    private bool canSpawn = false;
    private int attemptCount = 0;
    private int maxAttempts = 200;

    Collider2D collider;

    void Start()
    {
        probabilityNumber = probabilitySmall + probabilityMedium + probabilityLarge;
    }

    void Update()
    {
        timer += Time.deltaTime;
        collider = GetComponent<Collider2D>();
        RandomSpawner();
    }

    private void RandomSpawner()
    {
        if(timer > durationTimer && quantityObjects < 5)
        {
            probabilitySpawn = UnityEngine.Random.Range(1, probabilityNumber);
            randomPosition = CreateRandomPosition(GetComponent<Collider2D>());
            if (probabilitySpawn <= probabilitySmall)
                Instantiate(smallObject, randomPosition, Quaternion.identity);

            else if (probabilitySpawn < probabilitySmall + probabilityMedium + 1)
                Instantiate(mediumObject, randomPosition, Quaternion.identity);

            else
                Instantiate(largeObject, randomPosition, Quaternion.identity);
            quantityObjects++;
            timer = 0;
        }
    }

    Vector2 CreateRandomPosition(Collider2D collider)
    {
        Vector2 colliderCenter = collider.bounds.center;
        Vector2 colliderSize = collider.bounds.size / 2f;

        Vector2 randomPositionWhile = colliderCenter + new Vector2 (UnityEngine.Random.Range(-colliderSize.x, colliderSize.x), UnityEngine.Random.Range(-colliderSize.y, colliderSize.y));

        return randomPositionWhile;
    }

    private void CheckPossibleSpawn()
    {
        spawnPostion = Vector2.zero;
        while(!canSpawn && attemptCount < maxAttempts)
        {
           //spawnPostion = CreateRandomPosition 
        }
    }
}
