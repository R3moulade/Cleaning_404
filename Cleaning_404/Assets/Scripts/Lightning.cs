using UnityEngine;
using System.Collections;

public class FlashingSpotlight : MonoBehaviour
{
    public Light spotlight;
    public Renderer objectRenderer; // Assign the Renderer component of the object to be revealed
    public float flashDuration = 0.1f; // Adjust this to control how long the light appears
    public float timeBetweenFlashes = 0.2f; // Adjust this to control the delay between flashes
    public AudioClip[] thunderSounds; // Array of thunder sound effects
    public float minInterval = 0.5f;
    public float maxInterval = 2.0f;

    IEnumerator Start()
    {
        while (true)
        {
            // Turn on the spotlight
            spotlight.enabled = true;

            // Wait for the flash duration
            yield return new WaitForSeconds(flashDuration);

            // Turn off the spotlight
            spotlight.enabled = false;

            // Wait for a short delay before the next flash
            yield return new WaitForSeconds(timeBetweenFlashes);

            // Turn on the spotlight again for the second flash
            spotlight.enabled = true;

            // Wait for the flash duration
            yield return new WaitForSeconds(flashDuration);

            // Turn off the spotlight
            spotlight.enabled = false;

            // Choose a random thunder sound effect
            AudioClip thunderSound = thunderSounds[Random.Range(0, thunderSounds.Length)];

            // Play thunder sound effect
            if (thunderSound != null)
            {
                AudioSource.PlayClipAtPoint(thunderSound, transform.position);
            }

            // Make the object visible
            objectRenderer.enabled = true;

            // Wait for a short duration before hiding the object again
            yield return new WaitForSeconds(0.1f);

            // Hide the object
            objectRenderer.enabled = false;

            // Wait for a random interval before the next flash
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}