using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject objectToSpawn;
    public Transform spawnLocation;

    public string checkDirt;

    private bool objectSpawned = false;

    void Update() {
        int count = CountDirt(checkDirt);

        if (count == 0 && objectSpawned == false) {
                SpawnObject();
                objectSpawned = true;
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    int CountDirt(string tag) {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        return taggedObjects.Length;
    }

    void SpawnObject() {
        if (objectToSpawn != null && spawnLocation != null) {
            Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
        } else {
            Debug.LogError("Missing objectToSpawn or spawnLocation");
        }
    }
}