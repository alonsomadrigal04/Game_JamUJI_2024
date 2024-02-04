using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Timeline;

public class Player_behaviou : MonoBehaviour
{
    // ------ MOVEMENT ------
    Rigidbody2D rb;
    public float movement_speed;
    public bool canMove = false;

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
    public SpriteRenderer spriteRenderer;

    // ------- SOUNDS -------
    public AudioClip[] dashSounds;
    public AudioClip[] DyingSounds;
    public AudioClip[] ThrowSounds;
    public AudioClip winMusic;

    Rey_behaviour king_shit;
    public float timer_dance;
    public bool iniciado;
    public bool hasdance;

    public WinsAndLose winnlose;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        maincollider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        king_shit = FindObjectOfType<Rey_behaviour>();

        // ------- ANIMATION -------
        animator = GetComponent<Animator>();

        winnlose = FindObjectOfType<WinsAndLose>();
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
        animator.SetBool("isCarring", isCarry);

        CheckIsAlive();
        CheckDashInput();
        UpdateTimer();

        if(king_shit.theKingIsDead)
        {
            iniciado = true;
        }
        if(iniciado)
        {
            if(!isAlive)
            {
                winnlose.KingWins();
                iniciado = false;
                winnlose.win_bool = false;
            }

            timer_dance += Time.deltaTime;
            if(timer_dance >= 8.0f && !hasdance)
            {
                animator.SetBool("isDancing", true);
                hasdance= true;

                AudioSource[] fuentesDeAudio = FindObjectsOfType<AudioSource>();

                foreach (AudioSource fuenteDeAudio in fuentesDeAudio)
                {
                    fuenteDeAudio.Stop();
                }
                AudioSource.PlayClipAtPoint(winMusic, transform.position);

                GameObject primerHijo = transform.GetChild(0).gameObject;
                primerHijo.SetActive(false);

            }
        }
    }

    private void UpdateMovement()
    {
        if(canMove)
        {
            if (isAlive)
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

                // Guardar la direcci�n del jugador antes de realizar el dash
                if (dir != Vector2.zero)
                {
                    dashDirection = dir.normalized;
                }
            }

        }
    }

    private void UpdateTimer()
    {
        if (!isAlive)
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
            PlayRandomDashSound();
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

    private void PlayRandomDashSound()
    {
        if (dashSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, dashSounds.Length);
            AudioClip randomClip = dashSounds[randomIndex];

            // Play the selected dash sound
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }

    private void PlayRandomThrowinghSound()
    {
        if (ThrowSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, ThrowSounds.Length);
            AudioClip randomClip = ThrowSounds[randomIndex];

            // Play the selected dash sound
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }

    private void PlayRandomDyingSound()
    {
        if (DyingSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, DyingSounds.Length);
            AudioClip randomClip = DyingSounds[randomIndex];

            // Play the selected dash sound
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Small" || collision.gameObject.tag == "Medium" || collision.gameObject.tag == "Large")
        {
            isAlive = false;

            // Disable the Rigidbody and this script to prevent further movement
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            Destroy(collision.gameObject);
        }
    }



    public void CheckIsAlive()
    {
        if (!isAlive)
        {
            animator.SetBool("isDead", true);
            if (timer_animation <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
