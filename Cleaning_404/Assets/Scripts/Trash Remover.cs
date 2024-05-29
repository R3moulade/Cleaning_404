using System.Collections;
using UnityEngine;


public class TrashRemover : MonoBehaviour
{
    public string trashTag;
    public AudioSource trashSound;
    private float interactionDistance = 2f;

    void Update()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag(trashTag)) {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (hit.collider.gameObject.name == "Shower drain")
                    {
                        StartCoroutine(DestroyAndCountDirtWithDelay(hit.collider.gameObject, 5.0f));
                    }
                    else
                    {
                        Destroy(hit.collider.gameObject);
                        StartCoroutine(CountDirtNextFrame());
                    }
                    trashSound.Play();
                }
            }            
        }
    }

IEnumerator DestroyAndCountDirtWithDelay(GameObject obj, float delay)
{
    yield return new WaitForSeconds(delay);
    Destroy(obj);
    StartCoroutine(CountDirtNextFrame());
}

    IEnumerator CountDirtNextFrame()
    {
        yield return new WaitForEndOfFrame();
        GameManager.instance.CountDirt();
    }
}