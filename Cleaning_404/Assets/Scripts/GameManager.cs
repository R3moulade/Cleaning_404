using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject objectToSpawn;
    public Transform spawnLocation;

    public string[] checkDirtTags;

    private bool objectSpawned = false;

    void Update() {
        if (AllTagsHaveNoObjects() && !objectSpawned) {
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

    bool AllTagsHaveNoObjects() {
        foreach (string tag in checkDirtTags) {
            if (CountDirt(tag) > 0) {
                return false;
            }
        }
        return true;
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