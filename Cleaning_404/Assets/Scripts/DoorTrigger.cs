using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered by: " + other.gameObject.name); // Log the name of the entering object

        if (other.CompareTag("Player")) // Check if the object entering the trigger is the player
        {
            //Debug.Log("Player has entered the bathroom.");

            Door door = GetComponentInParent<Door>(); // Assuming the DoorTrigger is a child of the door object
            if (door != null)
            {
                door.ToggleDoor();
                //Debug.Log("Door locked. Prepare to die.");
                door.DisableInteraction(); // Disable door interaction
            }
        }
    }
}