using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    private int probabilitySmall = 10;
    private int probabilityMedium = 7;
    private int probabilityLarge = 5;
    private int probabilityNumber;
    private int probabilitySpawn;

    [HideInInspector]
    public int quantityObjects = 0;
    [HideInInspector]
    public int maxQuatityObjects = 5;

    private Vector2 randomPosition;

    public GameObject smallObject;
    public GameObject mediumObject;
    public GameObject largeObject;

    private int durationTimer;
    private float timer = 10;

    Collider2D collider;


    void Start()
    {
        probabilityNumber = probabilitySmall + probabilityMedium + probabilityLarge;
    }

    // Update is called once per frame
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
}
