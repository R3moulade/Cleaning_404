using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashRemover : MonoBehaviour
{
    public string trashTag;

    // Update is called once per frame
    void Update()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(trashTag)) {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    StartCoroutine(CountDirtNextFrame());
                }
            }
        }
    }

    IEnumerator CountDirtNextFrame()
    {
        yield return new WaitForEndOfFrame();
        GameManager.instance.CountDirt();
    }
}