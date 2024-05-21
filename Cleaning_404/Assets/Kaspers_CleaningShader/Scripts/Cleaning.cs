using UnityEngine;


[RequireComponent(typeof(MeshCollider))]

public class Cleaning : MonoBehaviour {

    [Header("Textures")]
    [Tooltip("The base texture of the object. Overrides the currently applied textures on the material.")]
    [SerializeField] private Texture2D baseTexture;
    [SerializeField] private Texture2D dirtTexture;
    [SerializeField] private Texture2D maskTexture;
    [SerializeField] private int maxPercentage = 95;

    [Header("Brush Settings")]
    [Tooltip("The type and size of brush to use for drawing on the mask texture.")]
    [SerializeField] private BrushType brushType = BrushType.Rectangle;
    
    [SerializeField] private int brushSize = 100;
    [SerializeField] private int brushWidth = 100; // New variable for rectangle width
    [SerializeField] private int brushHeight = 50; // New variable for rectangle height


    private Material myMaterial;
    private Texture2D appliedMaskTexture;

    private int totalPixelCount;
    private int blackPixelCount;


    

    private void Awake()
    {
        // Create new local material to prevent changes to the original material, based on the material of the object
        myMaterial = new Material(GetComponent<MeshRenderer>().material);
        myMaterial.name = "CleaningMaterial_" + gameObject.name;

        // Create a new texture for the dirt mask
        appliedMaskTexture = CopyTexture(maskTexture);
        appliedMaskTexture.name = "AppliedDirtMask";

        // Set the textures to the material
        myMaterial.SetTexture("_MainTex", baseTexture);
        myMaterial.SetTexture("_DirtTex", dirtTexture);
        myMaterial.SetTexture("_DirtMask", appliedMaskTexture);

        // Set the material to the object
        GetComponent<MeshRenderer>().material = myMaterial;

        // Initialize the total pixel count and the black pixel count
        totalPixelCount = appliedMaskTexture.width * appliedMaskTexture.height;
        blackPixelCount = 0;
    }

    private void Update()
    {
        // Check if the left mouse button is pressed using the new Input System
        if (Input.GetMouseButton(0))
        {
            // Get the mouse position from the new Input System and create a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {

                if(hit.collider.gameObject != gameObject)
                {
                    return;
                }
                
                // Get the mesh collider of the object (important for UV coordinates)
                MeshCollider meshCollider = hit.collider as MeshCollider;

                // Return if the mesh collider
                if (meshCollider == null || meshCollider.sharedMesh == null)
                    return;

                // Get the UV coordinates at the hit point
                Vector2 texCoord = hit.textureCoord;
                Debug.Log("UV Coordinates at cursor: " + texCoord);

                // Draw on the mask texture
                DrawOnMask(texCoord);

            }
        }  
    }

private void DrawOnMask(Vector2 uv)
{
    // Define the size of the brush
    int size = brushSize / 2;
    int halfWidth = brushWidth / 2; // Half of rectangle width
    int halfHeight = brushHeight / 2; // Half of rectangle height

    // Get the pixel coordinates from the UV coordinates
    int x = (int)(appliedMaskTexture.width * uv.x);
    int y = (int)(appliedMaskTexture.height * uv.y);
 
    // Draw on the mask texture based on the brush type
    for (int i = -halfWidth; i < halfWidth; i++)
    {
        for (int j = -halfHeight; j < halfHeight; j++)
        {
            // Use modulo to prevent out of bounds errors
            int appliedX = (x + i) % appliedMaskTexture.width;
            int appliedY = (y + j) % appliedMaskTexture.height;

            // Check if the pixel is not already black
            if (appliedMaskTexture.GetPixel(appliedX, appliedY) != Color.black)
            {
                if (brushType == BrushType.Circle)
                {
                    // Calculate the distance from the center of the circle
                    float distance = Mathf.Sqrt(i * i + j * j);

                    // Check if the pixel is within the circle
                    if (distance <= size)
                    {
                        // Draw a black pixel and increment the black pixel count
                        appliedMaskTexture.SetPixel(appliedX, appliedY, Color.black);
                        blackPixelCount++;
                    }
                }
                else if(brushType == BrushType.Square || brushType == BrushType.Rectangle)
                {
                    // Draw a black pixel and increment the black pixel count
                    appliedMaskTexture.SetPixel(appliedX, appliedY, Color.black);
                    blackPixelCount++;
                }
            }
        }
    }
    
    // Apply changes to the texture
    appliedMaskTexture.Apply();
    int cleanPercentage = (int)(((float)blackPixelCount / totalPixelCount) * 100);
    Debug.Log("Black Pixel Count: " + cleanPercentage + "%");

    if (cleanPercentage >= maxPercentage)
    {
        CleanedObject();
    }

}
    Texture2D CopyTexture(Texture2D source)
    {
        // Create a new texture
        Texture2D newTexture = new Texture2D(source.width, source.height);

        // Get pixels from the source texture
        Color[] pixels = source.GetPixels();

        // Set pixels to the destination texture
        newTexture.SetPixels(pixels);

        // Apply changes to the destination texture
        newTexture.Apply();

        return newTexture;
    }

    [System.Serializable]
    private enum BrushType
    {
        Square,
        Circle,
        Rectangle
    }
    private void CleanedObject()
    {
        gameObject.tag = "clean";
        Debug.Log("Cleaning completed on " + gameObject.name);
    }
}
