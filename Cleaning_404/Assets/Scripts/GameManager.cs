using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject spiderPentagramCandles;
    public GameObject pentagram;
    public BoxCollider candleSpawner;
    public GameObject candles;
    public AudioSource drawPentagram;
    public AudioSource lightCandles;

    private bool objectSpawned = false;
    
    

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        CountDirt();
    }

    void Update()
    {
    // Clear all dirt and trash objects for testing purposes
    if (Input.GetKeyDown(KeyCode.C))
    {
        GameObject[] dirtyObjects = GameObject.FindGameObjectsWithTag("dirty");
        foreach (GameObject obj in dirtyObjects)
        {
            obj.tag = "clean";
        }

        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("trash");
        foreach (GameObject obj in trashObjects)
        {
            Destroy(obj);
        }
        StartCoroutine(CountDirtNextFrame());
    }
    }
    IEnumerator CountDirtNextFrame()
    {
        yield return new WaitForEndOfFrame();
        CountDirt();
    }

    // Count all dirt and trash objects in the scene for the UI list and spawn the ritual circle
    public List<string> CountDirt() {
        GameObject[] dirtObjects = GameObject.FindGameObjectsWithTag("dirty");
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("trash");

        List<string> dirtObjectNames = new List<string>();
        List<string> trashObjectNames = new List<string>();

        foreach (GameObject obj in dirtObjects) {
            dirtObjectNames.Add(obj.name);
        }
        foreach (GameObject obj in trashObjects) {
            trashObjectNames.Add(obj.name);
        }

        if (dirtObjects.Length == 0 && trashObjects.Length == 0 && objectSpawned == false) {
            SpawnObject();
            objectSpawned = true;
        }
            UIManager.instance.DirtList(dirtObjectNames, trashObjectNames);


        return dirtObjectNames;
    }

    void SpawnObject() {
        pentagram.GetComponent<BoxCollider>().enabled = true;
        UIManager.instance.FadeInOutText("Make a ritual circle to banish the bathroom demon");
    }

    public void SpawnPentagram(){
        pentagram.GetComponent<Renderer>().enabled = true;
        pentagram.GetComponent<BoxCollider>().enabled = false;
        candleSpawner.enabled = true;
        drawPentagram.Play();
    }
    public void SpawnCandles(){
        candleSpawner.enabled = false;
        Instantiate(candles, candleSpawner.transform.position, Quaternion.Euler(90, 0, 0));
        UIManager.instance.FadeInOutText("");
        lightCandles.Play();
        Ending.Instance.SpiderExorcise(true);
    }
    
    
}