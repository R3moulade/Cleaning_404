using System.Collections;
using UnityEngine;


public class Door : MonoBehaviour
{
    private Animation doorAnimation;
    private bool isOpen = false;
    private bool canInteract = true; // Flag to determine if the door can be interacted with
    private bool isAnimating = false;

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
                Debug.Log("Close Room");
            }
            else
            {
                StartCoroutine(PlayAnimation("RoomDoorOpen"));
                Debug.Log("Open Room");
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