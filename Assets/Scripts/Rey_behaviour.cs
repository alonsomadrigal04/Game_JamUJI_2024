using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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


    // ------ PHASES ------
    public bool phase_1;
    public bool phase_2;
    public float phase_timer = 10;
    public float countdown_timer = 3;
    public int cnt_food = 0;
    private float shootTimer = 1.0f;


    // ------ SHOOTING ------
    public bool isShoot;
    public int small_cnt;
    public int medion_cnt;
    public int large_cnt;
    public float bullet_life;

    public BoxCollider2D colider_boca;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        UpdatePhases();

        UpdateTimer();
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
                    Debug.Log("Phase_change");
                    phase_1 = !phase_1;
                    phase_2 = !phase_2;
                    countdown_timer = 3.0f;
                    if (phase_2 && kingEated > 0)
                    {
                        cnt_food = kingEated;
                        kingDigested = kingEated;
                        kingEated = 0;
                        Debug.Log("Digested");
                    }
                    phase_timer = 5;
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
                hasSubtractedFood = false;
            }
        }
    }

    public void attackMode()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else if (cnt_food > 0)
        {
            ShootShit();
            cnt_food -= 1;
            shootTimer = 1.0f;
        }
        else
        {
            hasSubtractedFood = true;
        }
    }

    public void ShootShit()
    {
        float randomAngle = UnityEngine.Random.Range(70f, -70f);
        float radianAngle = randomAngle * Mathf.Deg2Rad;
        Vector2 shootDirection = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

        
        GameObject new_projectile = Instantiate(projectile, transform.position + new Vector3(2.0f, 0.0f, 0.0f), Quaternion.identity);
        Rigidbody2D rb_new_projectile = new_projectile.GetComponent<Rigidbody2D>();

        
        rb_new_projectile.velocity = shootDirection * speed;

        
        Destroy(new_projectile, bullet_life);

        isShoot = true;
    }




    public void checkLifeKing()
    {
        if (kingEated >= kingHungry)
        {
            Debug.Log("You WIN!");
        }
    }
}
