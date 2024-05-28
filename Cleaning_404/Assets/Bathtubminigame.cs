using System.Collections;
using UnityEngine;

public class Bathtubminigame : MonoBehaviour
{
    public Animation spiderWindup;
    public Animation jumpscare;
    public AudioSource spiderWindupAudio;
    public AudioSource jumpscareAudio;
    public GameObject showerCurtain;
    private ShowerCurtainController showerCurtainController;
    public Transform jumpscareSpider;
    public Transform bathtubSpider;
    private bool isMinigameTimerActive = false;


    void Start()
    {
        // Get the ShowerCurtainController script attached to the showerCurtain GameObject
        showerCurtainController = showerCurtain.GetComponent<ShowerCurtainController>();

    }
    void Update()
    {
        if (!showerCurtainController.isClosed && !spiderWindup.isPlaying && !jumpscare.isPlaying && !isMinigameTimerActive)
        {
            StartCoroutine(StartSpiderMinigameAtRandomInterval());
        }
    }

    IEnumerator StartSpiderMinigameAtRandomInterval()
    {
        isMinigameTimerActive = true;

        while (!showerCurtainController.isClosed && !spiderWindup.isPlaying && !jumpscare.isPlaying)
        {
            // Wait for a random interval between 1 and 10 seconds
            yield return new WaitForSeconds(Random.Range(30, 40));

            // If the shower curtain is not closed, start the SpiderMinigame coroutine
            if (!showerCurtainController.isClosed && !spiderWindup.isPlaying && !jumpscare.isPlaying)
            {
                StartCoroutine(SpiderMinigame());
            }
        }

        isMinigameTimerActive = false;
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
        if (showerCurtainController.isClosed)
        {
            spiderWindupAudio.Stop();
            spiderWindup.Stop();
            bathtubSpider.position = new Vector3(0, -4, 0);
            yield break;
        }

        yield return null;
    }

    // Play jumpscare
    bathtubSpider.position = new Vector3(0, -4, 0);
    jumpscare.Play();
    jumpscareAudio.Play();

    // Make the object dirty again
    SomeMethod();

    showerCurtainController.ToggleCurtain();
    // Wait for jumpscare to finish 1.5 seconds and then reset position of spider
    yield return new WaitForSeconds(1.6f);
    jumpscare.Stop();
    //reset spider position
    jumpscareSpider.position = new Vector3(0, -4, 0);
    
}
GameObject ChooseRandomCleanObject()
{
    // Get all GameObjects with the tag "clean"
    GameObject[] cleanObjects = GameObject.FindGameObjectsWithTag("clean");

    // If there are no clean objects, return null
    if (cleanObjects.Length == 0)
    {
        return null;
    }

    // Choose a random index
    int randomIndex = Random.Range(0, cleanObjects.Length);

    // Return the GameObject at the random index
    return cleanObjects[randomIndex];
}
void SomeMethod()
{
    // Get a random clean object
    GameObject randomCleanObject = ChooseRandomCleanObject();

    // If a clean object was found, make it dirty
    if (randomCleanObject != null)
    {
        MakeObjectDirty(randomCleanObject);
    }
}
    void MakeObjectDirty(GameObject obj)
    {
    // Get the Cleaning script attached to the object
    Cleaning cleaningScript = obj.GetComponent<Cleaning>();

    // If the Cleaning script is attached to the object
    if (cleaningScript != null)
    {
        // Create a new instance of the MaskTexture
        Texture2D newMaskTexture = Instantiate(cleaningScript.maskTexture);

        // Assign the new instance to the MaskTexture variable of the Cleaning script
        cleaningScript.maskTexture = newMaskTexture;
    }
        obj.tag = "dirty"; // Change the tag of the object to dirty
        StartCoroutine(DisplayDirtyText(obj));    
        }
        IEnumerator DisplayDirtyText(GameObject obj)
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(2.5f);

            // Display text that the object is dirty
            UIManager.instance.FadeInOutText(obj.name + " is dirty again");
            GameManager.instance.CountDirt();
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