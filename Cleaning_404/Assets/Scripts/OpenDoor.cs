using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public string doorTag = "Door"; // Tag to identify cabinet objects

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Use GetKeyDown to detect a single key press
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(doorTag)) // Check if the clicked object has the "Cabinet" tag
                {
                    Door door = hit.transform.GetComponent<Door>();
                    if (door != null)
                    {
                        door.ToggleDoor(); // Toggle the cabinet state
                    }
                }
            }
        }
    }
}
