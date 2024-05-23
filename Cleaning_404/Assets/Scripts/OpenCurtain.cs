using UnityEngine;

public class ShowerCurtainController : MonoBehaviour
{
    public GameObject showerCurtainOpen;  // Reference to the open state object
    public GameObject showerCurtainClosed; // Reference to the closed state object
    public float interactionDistance = 3.0f;  // Maximum distance to interact

    private Camera playerCamera;  // Reference to the main camera
    private bool isOpen = true;  // Tracks the current state

    void Start()
    {
        // Initialize the states
        UpdateCurtainState();

        // Get the main camera
        playerCamera = Camera.main;
    }

    // Update the curtain state based on the isOpen variable
    void UpdateCurtainState()
    {
        if (showerCurtainOpen != null && showerCurtainClosed != null)
        {
            showerCurtainOpen.SetActive(isOpen);
            showerCurtainClosed.SetActive(!isOpen);

            // Enable/disable colliders based on the state
            SetCollidersEnabled(isOpen);
        }
    }

    // Method to toggle the curtain state
    void ToggleCurtain()
    {
        isOpen = !isOpen;  // Toggle the state
        UpdateCurtainState();  // Update the visuals
    }

    // Enable/disable colliders of curtain objects based on the current state
    void SetCollidersEnabled(bool openState)
    {
        Collider[] openColliders = showerCurtainOpen.GetComponentsInChildren<Collider>();
        foreach (Collider collider in openColliders)
        {
            collider.enabled = openState;
        }

        Collider[] closedColliders = showerCurtainClosed.GetComponentsInChildren<Collider>();
        foreach (Collider collider in closedColliders)
        {
            collider.enabled = !openState;
        }
    }

    void Update()
    {
        // Detect pressing the E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cast a ray from the camera towards the forward direction
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
            {
                // Check if the raycast hits the curtain objects
                if (hit.transform == showerCurtainOpen.transform || hit.transform == showerCurtainClosed.transform)
                {
                    ToggleCurtain();  // Toggle the curtain when E is pressed and the cursor is on the curtain
                }
            }
        }
    }
}
