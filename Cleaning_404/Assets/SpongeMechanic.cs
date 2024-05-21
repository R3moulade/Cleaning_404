using UnityEngine;

public class SpongeMechanic : MonoBehaviour
{
    public GameObject sponge;

    private Vector3 spongeStartLocalPos;
    private Vector3 spongeStartLocalRotation;
    public LayerMask layerMask;

    private void Start()
    {
        spongeStartLocalPos = sponge.transform.localPosition;
        spongeStartLocalRotation = sponge.transform.localEulerAngles;
    }

    void Update()
    {
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
            sponge.transform.localPosition = spongeStartLocalPos;
            sponge.transform.localEulerAngles = spongeStartLocalRotation;
        }
    }
}