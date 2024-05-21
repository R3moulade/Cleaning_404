using UnityEngine;

public class Sponge : MonoBehaviour
{
    // Reference to the existing sponge object in the scene
    public GameObject sponge;

    [Header("Player's Left Hand Marker")]
    public Transform leftHandMarker;

    public LayerMask layerMask;

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    private void Start()
    {
        // Store the initial local position and rotation of the sponge relative to the left hand marker
        initialLocalPosition = sponge.transform.localPosition;
        initialLocalRotation = sponge.transform.localRotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                // Move and rotate the existing sponge object to the hit point and normal
                sponge.transform.parent = null; // Unparent the sponge
                sponge.transform.position = hit.point;
                sponge.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Set the sponge's parent back to the left hand marker
            sponge.transform.parent = leftHandMarker;

            // Reset the local position and rotation of the sponge relative to the left hand marker
            sponge.transform.localPosition = initialLocalPosition;
            sponge.transform.localRotation = initialLocalRotation;
        }
    }
}