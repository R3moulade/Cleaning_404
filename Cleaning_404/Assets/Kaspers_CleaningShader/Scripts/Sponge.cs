using UnityEngine;

public class Sponge : MonoBehaviour
{
    public GameObject parentObject;  // Parent object to be assigned in the inspector
    private GameObject sponge;

    private Vector3 spongeStartPos;
    private Vector3 spongeStartRotation;
    public LayerMask layerMask;

    private void Start()
    {
        // Ensure the parent object is assigned
        if (parentObject != null)
        {
            // Find the sponge GameObject by tag within the specified parent
            foreach (Transform child in parentObject.transform)
            {
                if (child.CompareTag("sponge"))
                {
                    sponge = child.gameObject;
                    break;
                }
            }

            if (sponge != null)
            {
                spongeStartPos = sponge.transform.position;
                spongeStartRotation = sponge.transform.eulerAngles;
            }
            else
            {
                Debug.Log("No GameObject found with the tag 'sponge' within the specified parent.");
            }
        }
        else
        {
            Debug.LogError("Parent object is not assigned.");
        }
    }

    void Update()
    {
        // Ensure the sponge was found before using it in the update logic
        if (sponge == null) return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                sponge.transform.parent = null;
                sponge.transform.position = hit.point;
                sponge.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            sponge.transform.parent = Camera.main.transform;
            sponge.transform.position = spongeStartPos;
            sponge.transform.eulerAngles = spongeStartRotation;
        }
    }
}
