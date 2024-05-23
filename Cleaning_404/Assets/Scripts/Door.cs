using UnityEngine;

public class Door : MonoBehaviour
{
    private Animation doorAnimation;
    private bool isOpen = false;
    private bool canInteract = true; // Flag to determine if the door can be interacted with

    void Start()
    {
        doorAnimation = GetComponent<Animation>();
    }

    public void ToggleDoor()
    {
        if (doorAnimation != null && canInteract)
        {
            if (isOpen)
            {
                doorAnimation.Play("RoomDoorClose");
            }
            else
            {
                doorAnimation.Play("RoomDoorOpen");
            }
            isOpen = !isOpen;
        }
    }

    public void DisableInteraction()
    {
        canInteract = false;
    }
}