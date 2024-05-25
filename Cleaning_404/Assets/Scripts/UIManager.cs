using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public string[] interactiveTags; // Tags to detect for color switch
    public Image whiteKnob; // Reference to the white knob image
    public Image yellowKnob; // Reference to the yellow knob image
    public TextMeshProUGUI pressEText; // Reference to the "Press E" text prompt
    public TextMeshProUGUI dirtListText;
    public TextMeshProUGUI trashListText;
    public TextMeshProUGUI cleanPercentageText;
    public TextMeshProUGUI objectCleanedText;
    public Animation objectCleanedAnimation;
    private Vector3 originalScale;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        originalScale = objectCleanedText.transform.localScale;
    }
    void Start()
    {
        // Ensure only white knob is initially active
        SetCrosshairColor(false);
        pressEText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hits a collider with any of the specified tags
            foreach (string interactiveTag in interactiveTags)
            {
                if (hit.collider.CompareTag(interactiveTag))
                {
                    pressEText.gameObject.SetActive(true);
                    pressEText.transform.position = transform.position + Vector3.up * 5f; // Adjust position as needed
                    SetCrosshairColor(true); // Switch to yellow knob color
                    return;
                }
            }
        }

        // If no object with the specified tag is hit, switch back to white knob color
        SetCrosshairColor(false);
         pressEText.gameObject.SetActive(false);

    }

    public void SetCrosshairColor(bool isYellow)
    {
        whiteKnob.gameObject.SetActive(!isYellow);
        yellowKnob.gameObject.SetActive(isYellow);
    }
    public void DirtList(List<string> dirtObjectNames, List<string> trashObjectNames)
    {
        // Convert the list to a string and display it
        if (dirtObjectNames.Count <= 3)
        {
            string dirtListString = string.Join("\n", dirtObjectNames);
            dirtListText.text = dirtListString;
        }
        else {
            dirtListText.text = dirtObjectNames.Count + " items left to clean";
        }


        string trashListString = string.Join("\n", trashObjectNames);
        trashListText.text = trashListString;
    }

    public void UpdateCleanPercentage(float cleanPercentage)
    {
        cleanPercentageText.text = cleanPercentage.ToString("F0") + "%";
    }
    public void ObjectCleaned(string objectCleanedName)
    {
        objectCleanedText.text = objectCleanedName + " cleaned";

        objectCleanedAnimation.Play();
    }
}