using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerMovement playerMovement; // Assuming PlayerMovement is the script attached to your character controller

    public AudioClip[] footstepSounds; // Array to hold footstep sounds
    public AudioClip crouchSound; // Sound effect for crouching
    public AudioClip tiptoeSound; // Sound effect for tiptoeing

    private bool isCrouching = false; // Flag to track if the player is crouching
    private bool isTiptoeing = false; // Flag to track if the player is tiptoeing

    private bool isPlayingFootstep = false; // Flag to track if a footstep sound is currently playing

    private float footstepInterval = 0.5f; // Interval between footstep sounds in seconds
    private float nextFootstepTime = 0f; // Time when the next footstep sound can be played

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Check for crouch input only when necessary
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
        {
            audioSource.PlayOneShot(crouchSound);
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
        {
            isCrouching = false;
        }

        // Check for tiptoe input only when necessary
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isTiptoeing)
        {
            audioSource.PlayOneShot(tiptoeSound);
            isTiptoeing = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isTiptoeing)
        {
            isTiptoeing = false;
        }

        // Check if the player is moving and enough time has passed since the last footstep
        bool isMoving = playerMovement.IsMoving();
        if (isMoving && Time.time >= nextFootstepTime && !isCrouching && !isTiptoeing)
        {
            // Play a random footstep sound from the array
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(footstepSound);

            // Set the time for the next footstep sound
            nextFootstepTime = Time.time + footstepInterval;
        }
    }
}