using System.Collections;
using UnityEngine;

public class Bathtubminigame : MonoBehaviour
{
    public Animation spiderWindup;
    public Animation jumpscare;
    public AudioSource spiderWindupAudio;
    public AudioSource jumpscareAudio;
    public GameObject showerCurtain;
    public GameObject objectToMakeDirty;
    private ShowerCurtainController showerCurtainController;

    void Start()
    {
        // Get the ShowerCurtainController script attached to the showerCurtain GameObject
        showerCurtainController = showerCurtain.GetComponent<ShowerCurtainController>();

        StartCoroutine(SpiderMinigame());
    }

IEnumerator SpiderMinigame()
{
    // Play animation1
    spiderWindup.Play();

    // Play audio source
    spiderWindupAudio.Play();

    // Wait for animation1 and audio source to finish
    while (spiderWindup.isPlaying)
    {
        // If shower curtain is closed, stop coroutine, audio and animation
        if (!showerCurtainController.isOpen)
        {
            spiderWindupAudio.Stop();
            spiderWindup.Stop();
            yield break;
        }

        yield return null;
    }

    // Play jumpscare
    jumpscare.Play();
    jumpscareAudio.Play();

    // Make the object dirty again
    MakeObjectDirty(objectToMakeDirty);

    // Display text that it's dirty
    Debug.Log(objectToMakeDirty.name + " is dirty again");
}

    void MakeObjectDirty(GameObject obj)
    {
        // Code to make the object dirty
        obj.GetComponent<Renderer>().material.color = Color.black; // Change the color of the object to black to represent that it's dirty
        obj.tag = "dirty"; // Change the tag of the object to dirty
    }
}
    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.O))
    //     {
    //         Vector3 cameraLookDirection = Camera.main.transform.forward;
    //         Vector3 cameraToObject = transform.position - Camera.main.transform.position;

    //         float angle = Vector3.Angle(cameraLookDirection, cameraToObject);

    //         if(Vector3.Dot(cameraLookDirection, cameraToObject) > 0)
    //         {
    //             Debug.Log("Object is to the front of the camera");
    //         }
    //         else
    //         {
    //             Debug.Log("Object is to the back the camera");
    //         }
    //     }
    // }