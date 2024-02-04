using System.Collections;
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
    public Sprite[] imageSmall;
    public Sprite[] imageMedium;
    public Sprite[] imageLarge;

    // ------ TIMER ------
    public float timer;
    public float durationTimer;

    // ------ QUANTITY ------
    public int quantityObjects = 0;
    public int maxQuatityObjects = 5;

    Collider2D collider2D;

    [SerializeField] private LayerMask _layerObjectCantSpawnOn;

    private void Awake()
    {
        if (instance == null)
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
        UpdateQuantityObjects();
    }

    private void UpdateQuantityObjects()
    {
        GameObject[] smallObjects = GameObject.FindGameObjectsWithTag("Small");
        GameObject[] mediumObjects = GameObject.FindGameObjectsWithTag("Medium");
        GameObject[] largeObjects = GameObject.FindGameObjectsWithTag("Large");

        quantityObjects = smallObjects.Length + mediumObjects.Length + largeObjects.Length;
    }


    public void Timer()
    {
        if (timer > durationTimer && quantityObjects < maxQuatityObjects)
        {
            probabilitySpawn = UnityEngine.Random.Range(1, probabilityNumber);
            Vector2 randomPosition = GetRandomPosition();

            if (probabilitySpawn <= probabilitySmall)
            {
                SpawnRandomObject(smallObject, imageSmall, randomPosition);
                quantityObjects++;
            }
            else if (probabilitySpawn <= probabilitySmall + probabilityMedium)
            {
                SpawnRandomObject(mediumObject, imageMedium, randomPosition);
                quantityObjects++;
            }
            else
            {
                SpawnRandomObject(largeObject, imageLarge, randomPosition);
                quantityObjects++;
            }

            timer = 0;
        }
        else if (timer > durationTimer)
        {
            // ... (código adicional si es necesario)
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void SpawnRandomObject(GameObject objectToSpawn, Sprite[] spriteArray, Vector2 position)
    {
        if (CheckIfPossibleToSpawn(position))
        {
            int randomIndex = UnityEngine.Random.Range(0, spriteArray.Length);
            Sprite selectedSprite = spriteArray[randomIndex];

            // Aquí puedes hacer algo con el sprite seleccionado, como asignarlo al objetoToSpawn
            objectToSpawn.GetComponent<SpriteRenderer>().sprite = selectedSprite;

            SpawnObject(objectToSpawn, position);
        }
    }


    private Vector2 GetRandomPosition()
    {
        float minX = collider2D.bounds.min.x;
        float maxX = collider2D.bounds.max.x;
        float minY = collider2D.bounds.min.y;
        float maxY = collider2D.bounds.max.y;

        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    private bool CheckIfPossibleToSpawn(Vector2 position)
    {
        float overlapRadius = 0.5f; // Ajusta este valor según sea necesario

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, overlapRadius, _layerObjectCantSpawnOn);

        return colliders.Length == 0;
    }


    private void SpawnObject(GameObject objectToSpawn, Vector2 position)
    {
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }
}
