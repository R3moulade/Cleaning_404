using UnityEngine;

public class ObjectSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float lastSpeed;
    private const float updateInterval = 0.001f; // Update speed every millisecond
    public float minPitch = 0.5f; // Minimum pitch
    public float maxPitch = 2f; // Maximum pitch
    public float minSpeed = 0f; // Minimum speed
    public float maxSpeed = 10f; // Maximum speed
    public float maxVolume = 1f; // Maximum volume
    public float minVolume = 0.1f; // Minimum volume

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if mouse button 1 (left mouse button) is held down
        if (Input.GetMouseButton(0))
        {
            // Calculate speed
            float speed = Mathf.Abs((transform.position - lastPosition).magnitude) / Time.deltaTime;
            // Update only once per millisecond
            if (Time.time - lastSpeed >= updateInterval)
            {
                lastSpeed = Time.time;
                // Adjust pitch based on speed
                float pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(minSpeed, maxSpeed, speed));
                audioSource.pitch = pitch;
                
                // Adjust volume based on speed
                float volume = Mathf.Lerp(minVolume, maxVolume, Mathf.InverseLerp(minSpeed, maxSpeed, speed));
                audioSource.volume = volume;
            }

            // Play sound if moving
            if (speed > 0f && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Stop playing sound when mouse button 1 is released
            audioSource.Stop();
        }

        // Update last position for next frame
        lastPosition = transform.position;
    }
}