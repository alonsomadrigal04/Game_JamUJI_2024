using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class Rey_behaviour : MonoBehaviour
{
    // ------ MOVEMENT ------
    Rigidbody2D rb;
    public GameObject projectile;
    public float speed;

    // ------ STATS ------
    public int kingEated = 0;
    public int kingHungry = 10;
    public int kingDigested = 0;
    private bool hasSubtractedFood = false;
    public Color startColor = Color.white;  // Initial color
    public Color endColor = Color.green;

    // ------ PHASES ------
    public bool phase_1;
    public bool phase_2;
    public float phase_timer = 10;
    public float countdown_timer = 3;
    public int cnt_food = 0;
    private float shootTimer = 1.0f;

    // ------ KING MOVEMENT ------
    private int left = 1;
    private int right = 2;
    private int up = 3;
    private int down = 4;

    // ------ KING MOVEMENT VECTOR POSTION ------
    private Vector2 leftPosition = new Vector2(-2.53f, 0);
    private Vector2 rightPosition = new Vector2(2.53f, 0);
    private Vector2 upPosition = new Vector2(0, 1.64f);
    private Vector2 downPosition = new Vector2(0, -1.72f);

    // ------ KING MOVEMENT VECTOR POSTION FRONT ------
    private Vector2 leftPositionFront = new Vector2(-1.76f, 0);
    private Vector2 rightPositionFront = new Vector2(1.76f, 0);
    private Vector2 upPositionFront = new Vector2(0, 0.93f);
    private Vector2 downPositionFront = new Vector2(0, -0.93f);

    // ------ KING MOVEMENT VECTOR ROTATION ------
    private Vector3 leftRotation = new Vector3(0, 0, 90);
    private Vector3 rightRotation = new Vector3(0, 0, -90);
    private Vector3 upRotation = new Vector3(0, 0, 0);
    private Vector3 downRotation = new Vector3(0, 0, -180);

    // ------ KING TIMER ------
    public float timer;
    public float durationTimer = 2;
    private bool activateTimer = false;

    private bool control = false;


    public Collider2D collider2D;
    public Collider2D mouthCollider;

    public SpriteRenderer mouthSpriteRender;
    public SpriteRenderer spriteRenderer;

    private int newPosition;
    private int actualPosition = 3;
    private bool movementDone = false;

    // ------ SHOOTING ------ 
    public bool isShoot;
    public int small_cnt;
    public int medion_cnt;
    public int large_cnt;
    public float bullet_life;
    public List<string> food_list;
    public float smallSize = 1.0f;
    public float mediumSize = 1.5f;
    public float largeSize = 2.0f;
    public GameObject smallProjectilePrefab;
    public GameObject mediumProjectilePrefab;
    public GameObject largeProjectilePrefab;

    public BoxCollider2D colider_boca;

    // ------ SHOOTING ------ 
    public Animator animator;
    public ParticleSystem particulasVomito;


    //------ IMAGES_BULLETS ------
    public Sprite[] smallPictures;
    public Sprite[] mediumPictures;
    public Sprite[] largePictures;

    //------ BOOLEANOS ------
    public bool theKingIsDead = false;
    public bool destoySound;
    public float destroySound_timer;

    //------ SOUNDS ------
    public AudioClip eatingSound;
    public AudioClip deadSound;
    public AudioClip[] destroyingBullets;
    public AudioClip[] burpsSounds;
    

    ////------ FINALDEAD ------
    public int CuantityOfBullets = 10;
    public int WavesCount = 5;  // Número de oleadas
    public int BulletsPerWave = 20;  // Número de balas por oleada

    public mouth_behaviour scrMouth; 


    public bool eated;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        
    }
    void Update()
    {

        if(control)
           OutMovement();
        if (activateTimer)
        {
            timer += Time.deltaTime;
        }
        UpdatePhases();

        checkLifeKing();

        UpdateTimer();

        Color lerpedColor = Color.Lerp(startColor, endColor, (float)kingDigested/ kingHungry);
        spriteRenderer.color = lerpedColor;

    }

    private void PlayRandomEatingSound()
    {
        AudioSource.PlayClipAtPoint(eatingSound, transform.position);
        
    }

    private void PlayRandomDBurpSound()
    {
        if (burpsSounds.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, burpsSounds.Length);
            AudioClip randomClip = burpsSounds[randomIndex];

            // Play the selected dash sound
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }

    void ReproducirParticulas()
    {
        // Verificar si el sistema de partículas es válido
        if (particulasVomito != null)
        {
            // Detener el sistema de partículas antes de reproducir
            particulasVomito.Stop();

            // Reproducir las partículas
            particulasVomito.Play();
        }
        else
        {
            Debug.LogError("El componente ParticleSystem no está asignado.");
        }
    }

    private void PlayRandomDyingSound()
    {
        if (destroyingBullets.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, destroyingBullets.Length);
            AudioClip randomClip = destroyingBullets[randomIndex];

            // Play the selected dash sound
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }

    public void UpdatePhases()
    {
        if (phase_1)
        {
            checkLifeKing();
        }
        else
        {
            attackMode();
        }

    }

    public void UpdateTimer()
    {
        if (!phase_2 && cnt_food <= 0)
        {
            if (countdown_timer <= 0)
            {
                if (phase_timer >= 0)
                {
                    phase_timer -= Time.deltaTime;
                }
                else if (phase_timer <= 0)
                {

                    phase_1 = !phase_1;
                    phase_2 = !phase_2;
                    countdown_timer = 3.0f;
                    if (phase_2 && kingEated > 0)
                    {
                        cnt_food = kingEated;
                        kingDigested += kingEated;
                        kingEated = 0;

                    }
                    phase_timer = 10;
                }
            }
            else
            {
                countdown_timer -= Time.deltaTime;
            }
        }
        else if(cnt_food == 0)
        {
            
            phase_1 = !phase_1;
            phase_2 = !phase_2;
        }
        else if (phase_2)
        {
            attackMode();
            if (cnt_food <= 0 && !hasSubtractedFood)
            {
                phase_1 = !phase_1;
                phase_2 = !phase_2;
                kingDigested -= 2;
                if(kingDigested <= 0)
                { kingDigested = 0; }
                //KingMovement();
                hasSubtractedFood = false;
            }
        }

    }

    public void KingDead()
    {
        theKingIsDead = true;
        animator.SetBool("isDead", true);

        AudioSource.PlayClipAtPoint(deadSound, transform.position);

        // Crear una explosión de balas
        for (int i = 0; i < CuantityOfBullets; i++)
        {
            // Seleccionar aleatoriamente el tamaño de la bala
            float randomSize = UnityEngine.Random.Range(0.0f, 1.0f);
            GameObject projectilePrefab;

            if (randomSize < 0.33f)
            {
                projectilePrefab = smallProjectilePrefab;
            }
            else if (randomSize < 0.66f)
            {
                projectilePrefab = mediumProjectilePrefab;
            }
            else
            {
                projectilePrefab = largeProjectilePrefab;
            }

            // Seleccionar aleatoriamente la dirección de la bala
            float randomAngle = UnityEngine.Random.Range(0f, -180f);
            float radianAngle = randomAngle * Mathf.Deg2Rad;
            Vector2 shootDirection = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

            // Instanciar la bala
            GameObject new_projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb_new_projectile = new_projectile.GetComponent<Rigidbody2D>();

            CameraShakeManager.instance.Screenshake(0.1f);

            ProjectileBehavior projectileBehavior = new_projectile.AddComponent<ProjectileBehavior>();
            //projectileBehavior.bulletLife = bullet_life;
            projectileBehavior.destroyingSounds = destroyingBullets;

            rb_new_projectile.velocity = shootDirection * 1.2f;

            // ... (puedes ajustar otras propiedades, como el color, la vida de la bala, etc.)
        }
    }


    public void AnimationControler()
    {
        //scrMouth = GetComponent<mouth_behaviour>();
        scrMouth.animatoring.SetBool("isEating", false);
        eated = false;
    }

    public void attackMode()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else if (cnt_food > 0)
        {
            // Cambia para coger un objeto aleatorio de food_list
            int randomIndex = UnityEngine.Random.Range(0, food_list.Count);
            String tagName = food_list[randomIndex];

            // Ajusta tamaño y velocidad según la etiqueta del objeto
            if (tagName == "Small")
            {
                ShootShit(smallProjectilePrefab, new Vector2(1.0f, 1.0f) * smallSize);

            }
            else if (tagName == "Medium")
            {
                ShootShit(mediumProjectilePrefab, new Vector2(1.0f, 1.0f) * mediumSize);
            }
            else if (tagName == "Large")
            {
                ShootShit(largeProjectilePrefab, new Vector2(1.0f, 1.0f) * largeSize);
            }

            food_list.RemoveAt(randomIndex);
            cnt_food -= 1;
            shootTimer = 1.0f;
        }
        else
        {
            hasSubtractedFood = true;
        }
    }

    public void ShootShit(GameObject projectilePrefab, Vector2 bullet_velocity)
    {
        float randomAngle = 0f;

        // Determine the angle based on the king's current position
        switch (actualPosition)
        {
            case 1: // Left
                randomAngle = UnityEngine.Random.Range(70f, -70f);
                break;
            case 2: // Right
                randomAngle = UnityEngine.Random.Range(-180f, -110f);
                break;
            case 3: // Up
                randomAngle = UnityEngine.Random.Range(-25f, -156f);
                break;
            case 4: // Down
                randomAngle = UnityEngine.Random.Range(160f, 200f);
                break;
            default:
                break;
        }

        float radianAngle = randomAngle * Mathf.Deg2Rad;
        Vector2 shootDirection = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

        CameraShakeManager.instance.Screenshake(0.9f);

        GameObject new_projectile = Instantiate(projectilePrefab, transform.position + new Vector3(0.0f, -0.1f, 0.0f), Quaternion.identity);

        PlayRandomDBurpSound();
        ReproducirParticulas();


        Rigidbody2D rb_new_projectile = new_projectile.GetComponent<Rigidbody2D>();
         
        rb_new_projectile.velocity = shootDirection * bullet_velocity;

        SpriteRenderer projectileRenderer = new_projectile.GetComponent<SpriteRenderer>();

        // Randomly select a sprite based on the type of projectile
        if (projectilePrefab == smallProjectilePrefab)
        {
            int randomIndex = UnityEngine.Random.Range(0, smallPictures.Length);
            projectileRenderer.sprite = smallPictures[randomIndex];
        }
        else if (projectilePrefab == mediumProjectilePrefab)
        {
            int randomIndex = UnityEngine.Random.Range(0, mediumPictures.Length);
            projectileRenderer.sprite = mediumPictures[randomIndex];
        }
        else if (projectilePrefab == largeProjectilePrefab)
        {
            int randomIndex = UnityEngine.Random.Range(0, largePictures.Length);
            projectileRenderer.sprite = largePictures[randomIndex];
        }

        // Attach ProjectileBehavior component to the projectile
        ProjectileBehavior projectileBehavior = new_projectile.AddComponent<ProjectileBehavior>();
        //projectileBehavior.bulletLife = bullet_life;
        projectileBehavior.destroyingSounds = destroyingBullets;



        isShoot = true;
    }


    public void checkLifeKing()
    {
        if (kingDigested >= kingHungry)
        {
            animator.SetBool("isDead", true);
            GameObject primerHijo = transform.GetChild(0).gameObject;
            primerHijo.SetActive(false);
        }
    }

    private void KingMovement()
    {
        Debug.Log("1 entra");
        switch (actualPosition)
        {
            case 1:
                Debug.Log("Caso01");
                transform.DOMoveX(transform.position.x - 0.80f, 3).OnComplete(tpMovement);
                break;
            case 2:
                Debug.Log("Caso02");
                transform.DOMoveX(transform.position.x + 0.80f, 3).OnComplete(tpMovement);
                break;
            case 3:
                Debug.Log("Caso03");
                transform.DOMoveY(transform.position.y + 0.80f, 3).OnComplete(tpMovement);
                break;
            case 4:
                Debug.Log("Caso04");
                transform.DOMoveY(transform.position.y - 0.80f, 3).OnComplete(tpMovement);
                break;
            default: break;
        }
    }

    private void tpMovement()
    {
        Debug.Log("2 preparacion tp");
        newPosition = UnityEngine.Random.Range(1, 5);
        if ((newPosition >= 1 && newPosition <= 4 && newPosition != actualPosition) && !movementDone)
        {
            spriteRenderer.enabled = false;
            mouthSpriteRender.enabled = false;
            collider2D.enabled = false;
            mouthCollider.enabled = false;
            Debug.Log("3 tp");
            actualPosition = newPosition;
            switch (newPosition)
            {
                case 1:
                    transform.position = leftPosition;
                    transform.DORotate(rightRotation, 0.1f);
                    movementDone = true;
                    Debug.Log("izq");
                    break;
                case 2:
                    transform.position = rightPosition;
                    transform.DORotate(rightRotation, 0.1f);
                    movementDone = true;
                    Debug.Log("der");
                    break;
                case 3:
                    transform.position = upPosition;
                    transform.DORotate(upRotation, 0.1f);
                    movementDone = true;
                    Debug.Log("up");
                    break;
                case 4:
                    transform.position = downPosition;
                    transform.DORotate(downRotation, 0.1f);
                    movementDone = true;
                    Debug.Log("down");
                    break;
                default: break;
            }
        }
        control = true;
    }

    private void OutMovement()
    {
        activateTimer = true;
        if(timer > durationTimer)
        {
            Debug.Log("4 salida");
            spriteRenderer.enabled = enabled;
            mouthSpriteRender.enabled = enabled;
            collider2D.enabled = enabled;
            mouthCollider.enabled = enabled;
            movementDone = false;
            switch (actualPosition)
            {
                case 1:
                    transform.DOMove(leftPositionFront, 3);
                    break;
                case 2:
                    transform.DOMove(rightPositionFront, 3);
                    break;
                case 3:
                    transform.DOMove(upPositionFront, 3);
                    break;
                case 4:
                    transform.DOMove(downPositionFront, 3);
                    break;
                default: break;
            }
            timer = 0;
            activateTimer = false;
            control = false;
        }
        
    }


}
