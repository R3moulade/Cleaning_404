using UnityEngine;

public class Door : MonoBehaviour
{
    private Animation doorAnimation;
    private bool isOpen = false;

    void Start()
    {
        doorAnimation = GetComponent<Animation>();
    }

    public void ToggleDoor()
    {
        if (doorAnimation != null)
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
}