using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2IneractTemplate : MonoBehaviour
{
    public string interactTag;

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {

            if (hit.collider.CompareTag(interactTag)) {
                
                if (Input.GetKeyDown(KeyCode.E)) {
                    Debug.Log("Works!");
                 // Whatever needs to happen when you press E! :D
                }

            }
        }
    }
}
