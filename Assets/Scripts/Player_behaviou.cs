using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviou : MonoBehaviour
{
    // ------ MOVEMENT ------
    Rigidbody2D rb;
    public int movement_speed;

    //------- CARRY --------
    public bool isCarry;
    public Collider2D maincollider;

    //------- LIFE --------
    public bool isAlive = true;

    //------- DASH --------
    private bool isDashing = false;
    private Vector2 dashDirection;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private float dashTimer = 0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        maincollider = gameObject.GetComponent<Collider2D>();
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

        CheckIsAlive();
        CheckDashInput();
    }

    private void UpdateMovement()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!isCarry)
        {
            rb.velocity = dir.normalized * movement_speed;
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
        movement_speed = 5;
    }

    private void CheckDashInput()
    {
        // Detectar la entrada para realizar el dash
        if (Input.GetKeyDown(KeyCode.C) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }
    }

    private void Dash()
    {
        // Aplicar velocidad de dash
        rb.velocity = dashDirection * dashSpeed;

        // Reducir el temporizador del dash
        dashTimer -= Time.deltaTime;

        // Finalizar el dash cuando el temporizador llega a cero
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
            Destroy(gameObject);
        }
    }
}
