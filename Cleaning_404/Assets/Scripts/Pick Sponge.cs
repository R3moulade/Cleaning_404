using UnityEngine;

public class SpongeSelector : MonoBehaviour
{
    public GameObject[] spongePrefabs;  // Assign sponge prefabs in the inspector
    public string[] spongeTags = { "sponge", "rag"};
    public Transform spawnPoint;  // Assign a transform in the inspector for the spawn point
    public Transform selectedParent;  // Assign a transform in the inspector for the parent GameObject

    private GameObject currentSponge;

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

        // Ensure spongePrefabs and spongeTags arrays are the same length
        if (spongePrefabs.Length != spongeTags.Length)
        {
            Debug.LogError("spongePrefabs and spongeTags arrays must be the same length.");
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
                for (int i = 0; i < spongeTags.Length; i++)
                {
                    if (hit.collider.CompareTag(spongeTags[i]))
                    {
                        Debug.Log("Hit collider with tag: " + spongeTags[i]);
                        if (i < spongePrefabs.Length)  // Ensure the index is within the bounds of spongePrefabs array
                        {
                            ReplaceSponge(spongePrefabs[i]);
                        }
                        else
                        {
                            Debug.LogError("Index out of range for spongePrefabs array. Make sure both arrays are of the same length.");
                        }
                        break;
                    }
                }
            }
        }
    }

    void ReplaceSponge(GameObject newSpongePrefab)
    {
        //  Destroy the current sponge object if it exists
        if (currentSponge != null)
        {
            Destroy(currentSponge);
        }

        // Instantiate the new sponge object at the spawn point's position and rotation, and set it as a child of the selected parent GameObject
        currentSponge = Instantiate(newSpongePrefab, spawnPoint.position, spawnPoint.rotation, selectedParent);
    }
}
