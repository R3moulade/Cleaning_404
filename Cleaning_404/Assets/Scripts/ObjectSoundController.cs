using UnityEngine;

public class ObjectSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float lastSpeed;
    private const float updateInterval = 0.001f; // Update speed every millisecond

    // Bubble particle system prefab
    public ParticleSystem bubbleParticleSystem;

    // Interpolation variables
    private float targetPitch;
    private float targetVolume;
    private float pitchVelocity;
    private float volumeVelocity;
    public float smoothTime = 0.1f; // Smoothing time for interpolation

    private ParticleSystem bubblesInstance; // Instance of the spawned bubble particle system
    private bool isCleaning = false; // Flag to track cleaning action

    // Speed parameters for adjusting pitch and volume
    public float minSpeed = 0f; // Minimum speed
    public float maxSpeed = 10f; // Maximum speed

    // Pitch and volume parameters
    public float minPitch = 0.5f; // Minimum pitch
    public float maxPitch = 2f; // Maximum pitch
    public float minVolume = 0.1f; // Minimum volume
    public float maxVolume = 1f; // Maximum volume

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if mouse button 1 (left mouse button) is pressed down
        {
            StartCleaning();
        }
        else if (Input.GetMouseButtonUp(0)) // Check if mouse button 1 (left mouse button) is released
        {
            StopCleaning();
        }

        if (isCleaning)
        {
            // Calculate speed
            float speed = Mathf.Abs((transform.position - lastPosition).magnitude) / Time.deltaTime;

            // Update only once per millisecond
            if (Time.time - lastSpeed >= updateInterval)
            {
                lastSpeed = Time.time;

                // Adjust pitch based on speed
                targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(minSpeed, maxSpeed, speed));

                // Adjust volume based on speed
                targetVolume = Mathf.Lerp(minVolume, maxVolume, Mathf.InverseLerp(minSpeed, maxSpeed, speed));
            }

            // Smoothly interpolate pitch and volume
            float smoothPitch = Mathf.SmoothDamp(audioSource.pitch, targetPitch, ref pitchVelocity, smoothTime);
            float smoothVolume = Mathf.SmoothDamp(audioSource.volume, targetVolume, ref volumeVelocity, smoothTime);

            // Apply smoothed values to AudioSource
            audioSource.pitch = smoothPitch;
            audioSource.volume = smoothVolume;

            // Play sound
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Stop sound if not cleaning
            audioSource.Stop();
        }

        // Update last position for next frame
        lastPosition = transform.position;
    }

    void StartCleaning()
    {
        if (!isCleaning)
        {
            isCleaning = true;
           
        }
    }

    void StopCleaning()
    {
        if (isCleaning)
        {
            isCleaning = false;
        }
    }
}