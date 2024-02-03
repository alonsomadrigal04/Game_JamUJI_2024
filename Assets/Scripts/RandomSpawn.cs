using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    private int probabilitySmall = 10;
    private int probabilityMedium = 7;
    private int probabilityLarge = 5;
    public int probabilityNumber;
    private int probabilitySpawn;

    private int quantityObjects = 0;

    private Vector2 randomPosition;

    public GameObject smallObject;
    public GameObject mediumObject;
    public GameObject largeObject;

    Collider2D collider;



    void Start()
    {
        probabilityNumber = probabilitySmall + probabilityMedium + probabilityLarge;
    }

    // Update is called once per frame
    void Update()
    {
        collider = GetComponent<Collider2D>();
    }

    private void RandomSpawner()
    {
        
        while (quantityObjects < 5) 
        {
            probabilitySpawn = Random.Range(1, probabilityNumber);
            if (probabilitySpawn <= probabilitySmall)
            {
                //Instantiate(smallObject,)
            }
            else if(probabilitySpawn < probabilitySmall + probabilityMedium + 1)
            {

            }
            else
            {

            }

            randomPosition = CreateRandomPosition(collider);

            
            
            quantityObjects++;
        }
    }

    Vector2 CreateRandomPosition(Collider2D collider)
    {
        Vector2 colliderCenter = collider.bounds.center;
        Vector2 colliderSize = collider.bounds.size;

        Vector2 randomPositionWhile = colliderCenter + new Vector2 (Random.Range(-colliderSize.x, colliderSize.x), Random.Range(-colliderSize.y, colliderSize.y));

        return randomPositionWhile;
    }
}
