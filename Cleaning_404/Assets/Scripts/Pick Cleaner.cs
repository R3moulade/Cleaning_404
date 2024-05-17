using UnityEngine;

public class ModelSelector : MonoBehaviour
{
    public GameObject[] cleanerPrefabs;  // Assign cleaner prefabs in the inspector
    public string[] cleanerTags = { "universal", "decalcifier", "glass_cleaner", "toilet_cleaner" };
    public Transform spawnPoint;  // Assign a transform in the inspector for the spawn point
    public Transform selectedParent;  // Assign a transform in the inspector for the parent GameObject

    private GameObject currentCleaner;

    void Start()
    {
        // Ensure a spawn point is assigned
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not assigned!");
        }

        // Ensure a parent transform is assigned
        if (selectedParent == null)
        {
            Debug.LogError("Selected parent not assigned!");
        }

        // Ensure cleanerPrefabs and cleanerTags arrays are the same length
        if (cleanerPrefabs.Length != cleanerTags.Length)
        {
            Debug.LogError("cleanerPrefabs and cleanerTags arrays must be the same length.");
        }
    }

    void Update()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Check if the ray hits a collider with any of the specified tags
                for (int i = 0; i < cleanerTags.Length; i++)
                {
                    if (hit.collider.CompareTag(cleanerTags[i]))
                    {
                        Debug.Log("Hit collider with tag: " + cleanerTags[i]);
                        if (i < cleanerPrefabs.Length)  // Ensure the index is within the bounds of cleanerPrefabs array
                        {
                            ReplaceCleaner(cleanerPrefabs[i]);
                        }
                        else
                        {
                            Debug.LogError("Index out of range for cleanerPrefabs array. Make sure both arrays are of the same length.");
                        }
                        break;
                    }
                }
            }
        }
    }

    void ReplaceCleaner(GameObject newCleanerPrefab)
    {
     //  Destroy the current cleaner object if it exists
        if (currentCleaner != null)
         {
         Destroy(currentCleaner);
          }

        // Instantiate the new cleaner object at the spawn point's position and rotation, and set it as a child of the selected parent GameObject
        currentCleaner = Instantiate(newCleanerPrefab, spawnPoint.position, spawnPoint.rotation, selectedParent);
    }
}
