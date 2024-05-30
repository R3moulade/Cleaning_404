using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    // Track the current fullscreen state
    private bool isFullscreen;

    void Start()
    {
        // Initialize the fullscreen state
        isFullscreen = Screen.fullScreen;
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the fullscreen state
            isFullscreen = !isFullscreen;

            // Apply the fullscreen state
            Screen.fullScreen = isFullscreen;
        }
    }
}