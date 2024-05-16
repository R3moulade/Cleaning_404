using UnityEngine;

public class ModelSelector : MonoBehaviour
{
    public string[] cleanerTags = { "universal", "decalcifier", "glass_cleaner", "toilet_cleaner" };

    private GameObject defaultCleanerObject;
    private GameObject currentCleaner;

    void Start()
    {
        // Find the default cleaner object based on the "Cleaner" tag
        defaultCleanerObject = GameObject.FindGameObjectWithTag("Cleaner");
        if (defaultCleanerObject == null)
        {
            Debug.LogError("Default cleaner object not found!");
        }
    }

    void Update()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit something!");

            
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Check if the ray hits a collider with any of the specified tags
            foreach (string cleanerTag in cleanerTags)
            {
                if (hit.collider.CompareTag(cleanerTag))
                {
                    Debug.Log("Hit collider with tag: " + cleanerTag);
                    ReplaceCleaner(hit.collider.gameObject);
                    break;
                }
            }
            }
        }
    }

    void ReplaceCleaner(GameObject newCleanerPrefab)
    {
        // Destroy the current cleaner object if it exists
        if (currentCleaner != null)
        {
            Destroy(currentCleaner);
        }

        // Deactivate the default cleaner if it's active
        if (defaultCleanerObject.activeSelf)
        {
            defaultCleanerObject.SetActive(false);
        }

        // Instantiate the new cleaner object at the same position and rotation as the default cleaner
        currentCleaner = Instantiate(newCleanerPrefab, defaultCleanerObject.transform.position, defaultCleanerObject.transform.rotation);
    }
}