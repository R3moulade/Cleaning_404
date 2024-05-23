using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public string doorTag = "Door"; // Tag to identify door objects

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(doorTag)) // Check if the clicked object has the "Door" tag
                {
                    Door door = hit.transform.GetComponent<Door>();
                    if (door != null)
                    {
                        door.ToggleDoor(); // Toggle the door state
                    }
                }
            }
        }
    }
}