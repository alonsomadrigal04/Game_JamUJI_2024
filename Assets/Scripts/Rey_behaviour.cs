using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rey_behaviour : MonoBehaviour
{
    // ------ MOVEMENT ------
    [SerializeField]
    Rigidbody2D rb;
    public GameObject projectile;
    public float speed;

    // ------ PHASES ------
    public bool phase_1;
    public bool phase_2;
    public int cnt_food;

    // ------ SHOOTING ------
    public bool isShoot;

    public BoxCollider2D colider_boca;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colider_boca = transform.Find("colider_boca").GetComponent<BoxCollider2D>();

    }
    void Update()
    {
        UpdatePhases();
    }
    public void UpdatePhases()
    {
        if (phase_1)
        {

        }
    }

    public void ShootShit()
    {
        GameObject new_projectile = Instantiate(projectile, transform.position + new Vector3(0.0f, 0.0f, 10.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        Rigidbody2D rb_new_projectile = new_projectile.GetComponent<Rigidbody2D>();

        rb_new_projectile.velocity = new Vector2(1.0f, 1.0f) * speed;
        // Destroy(new_projectile, 5.0f);

        isShoot = true;


    }
}
