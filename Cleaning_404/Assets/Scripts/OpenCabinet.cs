using UnityEngine;

public class OpenCabinet : MonoBehaviour
{
    public string doorTag = "Cabinet"; // Tag to identify cabinet objects

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(doorTag)) // Check if the clicked object has the "Cabinet" tag
                {
                    Cabinet cabinet = hit.transform.GetComponent<Cabinet>();
                    if (cabinet != null)
                    {
                        cabinet.ToggleCabinet(); // Toggle the cabinet state
                    }
                }
            }
        }
    }
}