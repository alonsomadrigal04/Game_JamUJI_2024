using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float bulletLife;
    public AudioClip[] destroyingSounds;

    private bool hasPlayedSound = false;
    public Rigidbody2D rb;

    public Vector2 velocity;


    private void Start()
    {
        // Destroy the projectile after bulletLife seconds
        Destroy(gameObject, bulletLife);
        rb = GetComponent<Rigidbody2D>();
        velocity= rb.velocity;

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
        if(rb.velocity == new Vector2(0.0f, 0.0f))
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

            // Play the selected sound
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
        }
    }
}
