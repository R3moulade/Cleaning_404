using UnityEngine;

public class KasperOriginalSponge: MonoBehaviour
{
    public GameObject sponge;

    private Vector3 spongeStartPos;
    private Vector3 spongeStartRotation;
    public LayerMask layerMask;

    private void Start()
    {
        spongeStartPos = sponge.transform.position;
        spongeStartRotation = sponge.transform.eulerAngles;
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
            sponge.transform.position = spongeStartPos;
            sponge.transform.eulerAngles = spongeStartRotation;
        }
    }
}