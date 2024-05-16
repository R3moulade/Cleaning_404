using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Update()
    {
        // Create a ray from the camera to the mouse cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform a raycast to determine the intersection point with a plane at z = 1
        // You can adjust the depth (z-value) as needed
        float distance = 0f; // Adjust this distance if needed
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, distance));
        if (plane.Raycast(ray, out float hitDistance))
        {
            // Get the intersection point
            Vector3 worldPosition = ray.GetPoint(hitDistance);

            // Update the position of the crosshair to the intersection point
            transform.position = worldPosition;

            // Make the sprite face the camera
            transform.LookAt(Camera.main.transform);
        }
    }
}