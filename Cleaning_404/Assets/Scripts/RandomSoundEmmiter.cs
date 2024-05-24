using UnityEngine;

public class RandomSoundEmitter : MonoBehaviour
{
    public AudioClip[] soundClips; // Array to hold all the sound clips you want to play
    public float minTimeBetweenSounds = 1f; // Minimum time between sounds
    public float maxTimeBetweenSounds = 3f; // Maximum time between sounds
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if there are any sound clips assigned
        if (soundClips.Length == 0)
        {
            Debug.LogWarning("No sound clips assigned to RandomSoundEmitter script on GameObject " + gameObject.name);
        }

        // Start emitting sounds after a random initial delay
        Invoke("PlayRandomSoundWithDelay", Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));
    }

    // Function to play a random sound clip with a random time delay
    private void PlayRandomSoundWithDelay()
    {
        // Play a random sound
        PlayRandomSound();

        // Schedule the next sound emission after a random time interval
        Invoke("PlayRandomSoundWithDelay", Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));
    }

    // Function to play a random sound clip
    public void PlayRandomSound()
    {
        // Check if there are any sound clips assigned
        if (soundClips.Length > 0)
        {
            // Choose a random index from the soundClips array
            int randomIndex = Random.Range(0, soundClips.Length);

            // Play the sound clip at the random index
            audioSource.PlayOneShot(soundClips[randomIndex]);
        }
        else
        {
            Debug.LogWarning("No sound clips assigned to RandomSoundEmitter script on GameObject " + gameObject.name);
        }
    }
}