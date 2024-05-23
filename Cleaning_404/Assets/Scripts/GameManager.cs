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
        CountDirt("dirty");
    }

public List<string> CountDirt(string tag) {
    GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
    List<string> objectNames = new List<string>();

    foreach (GameObject obj in taggedObjects) {
        objectNames.Add(obj.name);
    }

    if (taggedObjects.Length == 0 && objectSpawned == false) {
        SpawnObject();
        objectSpawned = true;
    }
        UIManager.instance.DirtList(objectNames);


    return objectNames;
}

    void SpawnObject() {
        if (objectToSpawn != null && spawnLocation != null) {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
        } else {
            Debug.LogError("Missing objectToSpawn or spawnLocation");
        }
    }
}