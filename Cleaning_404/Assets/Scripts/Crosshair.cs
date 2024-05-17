using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public string[] interactiveTags; // Tags to detect for color switch
    public Image whiteKnob; // Reference to the white knob image
    public Image yellowKnob; // Reference to the yellow knob image

    void Start()
    {
        // Ensure only white knob is initially active
        SetCrosshairColor(false);
    }

    void Update()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hits a collider with any of the specified tags
            foreach (string interactiveTag in interactiveTags)
            {
                if (hit.collider.CompareTag(interactiveTag))
                {
                    SetCrosshairColor(true); // Switch to yellow knob color
                    return;
                }
            }
        }

        // If no object with the specified tag is hit, switch back to white knob color
        SetCrosshairColor(false);
    }

    public void SetCrosshairColor(bool isYellow)
    {
        whiteKnob.gameObject.SetActive(!isYellow);
        yellowKnob.gameObject.SetActive(isYellow);
    }
}