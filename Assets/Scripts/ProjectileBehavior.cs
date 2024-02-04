using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public AudioClip[] destroyingSounds;
    private bool hasPlayedSound = false;
    public Rigidbody2D rb;
    public Vector2 velocity;
    public float bulletLife;

    private void Start()
    {
        // Genera un valor aleatorio entre 5 y 7 para bulletLife
        float randomBulletLife = Random.Range(5.0f, 7.0f);
        bulletLife = randomBulletLife;

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
