using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathtubminigame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            Vector3 cameraLookDirection = Camera.main.transform.forward;
            Vector3 cameraToObject = transform.position - Camera.main.transform.position;

            float angle = Vector3.Angle(cameraLookDirection, cameraToObject);

            if(Vector3.Dot(cameraLookDirection, cameraToObject) > 0)
            {
                Debug.Log("Object is to the front of the camera");
            }
            else
            {
                Debug.Log("Object is to the back the camera");
            }
        }
    }
}
