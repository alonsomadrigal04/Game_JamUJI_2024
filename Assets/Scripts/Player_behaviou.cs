using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviou : MonoBehaviour
{
    // ------ MOVEMENT ------
    Rigidbody2D rb;
    public float movement_speed;

    //------- CARRY --------
    public bool isCarry;
    public Collider2D maincollider;

    //------- LIFE --------
    public bool isAlive = true;
    public float timer_animation = 2.0f;

    //------- DASH --------
    private bool isDashing = false;
    private Vector2 dashDirection;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private float dashTimer = 0f;
    

    // ------- ANIMATION -------
    public Animator animator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        maincollider = gameObject.GetComponent<Collider2D>();

        // ------- ANIMATION -------
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (isDashing)
        {
            Dash();
        }
        else
        {
            UpdateMovement();
        }

        animator.SetBool("IsMoving", rb.velocity.magnitude > 0);

        CheckIsAlive();
        CheckDashInput();
        UpdateTimer();
    }

    private void UpdateMovement()
    {
        if(isAlive)
        {
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (!isCarry)
            {
                rb.velocity = dir.normalized * 1.5f;
            }
            else
            {
                rb.velocity = dir.normalized * movement_speed;
            }

            // Guardar la dirección del jugador antes de realizar el dash
            if (dir != Vector2.zero)
            {
                dashDirection = dir.normalized;
            }
            
        }
    }

    private void UpdateTimer()
    {
        if(!isAlive)
        {
            timer_animation -= Time.deltaTime;
        }
    }

    private void CheckDashInput()
    {
        // Detectar la entrada para realizar el dash
        if (Input.GetKeyDown(KeyCode.M) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }
    }

    private void Dash()
    {
        rb.velocity = dashDirection * dashSpeed;

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0f)
        {
            isDashing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CONTACTO");
        if (collision.gameObject.tag == "Small" || collision.gameObject.tag == "Medium" || collision.gameObject.tag == "Large")
        {
            isAlive = false;
        }
    }

    public void CheckIsAlive()
    {
        if (!isAlive)
        {
            animator.SetBool("isDead", true);
            if(timer_animation <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
