using UnityEngine;

public class Sponge : MonoBehaviour
{
    public GameObject parentObject;
    private GameObject sponge;

    private Vector3 spongeStartPos;
    private Vector3 spongeStartRotation;
    public LayerMask layerMask;

    private bool isSpongeChildFound = false;

    private void Start()
    {
        // Check if the parent object has a child with the tag "sponge"
        foreach (Transform child in parentObject.transform)
        {
            if (child.CompareTag("sponge"))
            {
                sponge = child.gameObject;
                isSpongeChildFound = true;
                break;
            }
        }

        // If the sponge child is found, initialize its start position and rotation
        if (isSpongeChildFound)
        {
            spongeStartPos = sponge.transform.position;
            spongeStartRotation = sponge.transform.eulerAngles;
        }
    }

    void Update()
    {
        // Only proceed if the sponge child is found
        if (!isSpongeChildFound)
            return;

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
            sponge.transform.position = spongeStartPos;
            sponge.transform.eulerAngles = spongeStartRotation;
        }
    }
}