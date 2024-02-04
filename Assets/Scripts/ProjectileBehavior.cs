using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ProjectileBehavior : MonoBehaviour
{
    public AudioClip[] destroyingSounds;
    private bool hasPlayedSound = false;
    public Rigidbody2D rb;
    public Vector2 velocity;
    public float bulletLife;

    public float timer_muerte;
    Vector2 death = new Vector2(0.0001f, 0.00001f);

    private void Start()
    {
        // Genera un valor aleatorio entre 5 y 7 para bulletLife
        float randomBulletLife = Random.Range(5.0f, 7.0f);
        bulletLife = randomBulletLife;
        timer_muerte = bulletLife;

        // Destruye el proyectil después de bulletLife segundos
        Destroy(gameObject, bulletLife);
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
    }

    private void OnDestroy()
    {
        if (!hasPlayedSound)
        {
            PlayRandomDyingSound();
            hasPlayedSound = true;
        }
    }

    private void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            rb.velocity += new Vector2(1.2f, 1.1f);
        }

        if(timer_muerte <=1.0f)
        {
            DeathAnimation();
        }

        timer_muerte -= Time.deltaTime;
    }

    private void DeathAnimation()
    {
        transform.DOScale(death, 0.2f);
    }


    private void PlayRandomDyingSound()
    {
        if (destroyingSounds.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, destroyingSounds.Length);
            AudioClip randomClip = destroyingSounds[randomIndex];

            // Reproduce el sonido seleccionado
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }
}
