using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject objectToSpawn;
    public Transform spawnLocation;

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
        if (objectToSpawn != null && spawnLocation != null) {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
        } else {
            Debug.LogError("Missing objectToSpawn or spawnLocation");
        }
    }
    
}