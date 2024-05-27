using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door Instance { get; private set; } // Singleton instance

    private Animation doorAnimation;
    private bool isOpen = false;
    private bool canInteract = true; // Flag to determine if the door can be interacted with
    private bool isAnimating = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        doorAnimation = GetComponent<Animation>();
    }

    public void ToggleDoor()
    {
        if (doorAnimation != null && !isAnimating)
        {
            if (isOpen)
            {
                StartCoroutine(PlayAnimation("RoomDoorClose"));
            }
            else
            {
                StartCoroutine(PlayAnimation("RoomDoorOpen"));
            }
            isOpen = !isOpen; // Toggle the state
        }
    }

    private IEnumerator PlayAnimation(string animationName)
    {
        isAnimating = true;
        doorAnimation.Play(animationName);
        yield return new WaitForSeconds(doorAnimation[animationName].length);
        isAnimating = false;
    }

    public void DisableInteraction()
    {
        canInteract = false;
    }
}