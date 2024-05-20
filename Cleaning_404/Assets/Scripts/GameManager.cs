using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string checkDirt;

    void Update() {
        int count = CountDirt(checkDirt);
    }

    private void Awake() {
        if (instance == null) {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    int CountDirt(string tag) {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

        return taggedObjects.Length;
    }
}
