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
    [Header("QUANTITY")]
    public int maxQuatityObjects = 5;

    // ------ OBJECTS ------
    public GameObject smallObject;
    public GameObject mediumObject;
    public GameObject largeObject;
    public List<GameObject> colObjectList;

    // ------ SPAWN ------
    [Header("SPAWN")]
    private Vector2 randomPosition;
    private Vector2 spawnPostion;
    public int durationTimer = 3;
    public float timer;
    private bool canSpawn = true;
    int layerCheck;



    Collider2D spawnColliderObject;

    Collider2D collider;

    void Start()
    {
        
        layerCheck = LayerMask.NameToLayer("SpawnCheck");
    }

    void Update()
    {
        timer += Time.deltaTime;
        collider = GetComponent<Collider2D>();
        RandomSpawner();
        
    }

    private void RandomSpawner()
    {
        if(timer > durationTimer && quantityObjects < maxQuatityObjects)
        {
            if(canSpawn)
            {
                probabilitySpawn = UnityEngine.Random.Range(1, probabilityNumber);
                randomPosition = CreateRandomPosition(GetComponent<Collider2D>());
                if (probabilitySpawn <= probabilitySmall)
                {
                    GameObject newObjectSmall = Instantiate(smallObject, randomPosition, Quaternion.identity);
                    CheckPossibleSpawn(newObjectSmall);
                }
                else if (probabilitySpawn < probabilitySmall + probabilityMedium + 1)
                {
                    GameObject newObjectMedium = Instantiate(mediumObject, randomPosition, Quaternion.identity);
                    CheckPossibleSpawn(newObjectMedium);
                }
                else
                {
                    GameObject newObjectLarge = Instantiate(largeObject, randomPosition, Quaternion.identity);
                    CheckPossibleSpawn(newObjectLarge);
                }
                quantityObjects++;
                timer = 0;
            }
        }
    }

    Vector2 CreateRandomPosition(Collider2D collider)
    {
        Vector2 colliderCenter = collider.bounds.center;
        Vector2 colliderSize = collider.bounds.size / 2f;

        Vector2 randomPositionWhile = colliderCenter + new Vector2 (UnityEngine.Random.Range(-colliderSize.x, colliderSize.x), UnityEngine.Random.Range(-colliderSize.y, colliderSize.y));

        return randomPositionWhile;
    }

    private void CheckPossibleSpawn(GameObject objectToSpawn)
    {
        canSpawn = false;
        int attemptCount = 0;
        int maxAttempts = 200;
        
        while(!canSpawn && attemptCount < maxAttempts)
        {
            Debug.Log("Preparando item");
            bool isTouchingCollider = false;
            foreach (GameObject spawnObject in colObjectList)
            {
                Collider2D colliderNewObject = spawnObject.GetComponent<Collider2D>();
                Collider2D colObjectToSpawn = objectToSpawn.GetComponent<Collider2D>();
                isTouchingCollider = false;
                
                if(colObjectToSpawn.IsTouchingLayers(layerCheck))
                {
                    Debug.Log("3");
                    isTouchingCollider = true;
                    break;
                }
            }
                        

            if(!isTouchingCollider)
            {
                Debug.Log("4");
                canSpawn = true;
                colObjectList.Add(objectToSpawn);
                break;
            }

            attemptCount++;
        }
        Debug.Log("5");
        return;

    }
}
