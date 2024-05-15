using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MeshCollider))]

public class Cleaning : MonoBehaviour {

    [Header("Textures")]
    [Tooltip("The base texture of the object. Overrides the currently applied textures on the material.")]
    [SerializeField] private Texture2D baseTexture;
    [SerializeField] private Texture2D dirtTexture;
    [SerializeField] private Texture2D maskTexture;

    [Header("Brush Settings")]
    [Tooltip("The type and size of brush to use for drawing on the mask texture.")]
    [SerializeField] private BrushType brushType = BrushType.Circle;
    [SerializeField] private int brushSize = 100;

    private Material myMaterial;
    private Texture2D appliedMaskTexture;

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
    }

    private void Update()
    {
        // Check if the left mouse button is pressed using the new Input System
        if (Mouse.current.leftButton.IsPressed())
        {
            // Get the mouse position from the new Input System and create a ray
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
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

        // Get the pixel coordinates from the UV coordinates
        int x = (int)(appliedMaskTexture.width * uv.x);
        int y = (int)(appliedMaskTexture.height * uv.y);
 
        // Draw on the mask texture based on the brush type
        for (int i = -size; i < size; i++)
        {
            for (int j = -size; j < size; j++)
            {
                // Use modulo to prevent out of bounds errors
                int appliedX = (x + i) % appliedMaskTexture.width;
                int appliedY = (y + j) % appliedMaskTexture.height;

                if (brushType == BrushType.Circle)
                {
                    // Calculate the distance from the center of the circle
                    float distance = Mathf.Sqrt(i * i + j * j);

                    // Check if the pixel is within the circle
                    if (distance <= size)
                    {
                        appliedMaskTexture.SetPixel(appliedX, appliedY, Color.black);
                    }
                }
                else if(brushType == BrushType.Square)
                {
                    appliedMaskTexture.SetPixel(appliedX, appliedY, Color.black);
                }
            }
        }
        
        // Apply changes to the texture
        appliedMaskTexture.Apply();
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
        Circle
    }
}
