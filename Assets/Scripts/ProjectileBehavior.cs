using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float bulletLife;
    public AudioClip[] destroyingSounds;

    private bool hasPlayedSound = false;

    private void Start()
    {
        // Destroy the projectile after bulletLife seconds
        Destroy(gameObject, bulletLife);
    }

    private void OnDestroy()
    {
        if (!hasPlayedSound)
        {
            PlayRandomDyingSound();
            hasPlayedSound = true;
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
