using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag = 6f;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Head Bobbing")]
    public float bobbingSpeed = 0.5f; // Speed of the bobbing
    public float bobbingAmount = 0.02f; // Amount of bobbing
    public float midpoint = 2.0f; // Default camera height

    private float timer = 0.0f;
    private Transform cameraTransform;
    private Transform cameraHolder;
    private float originalY;
    private bool isTiptoeing = false;
    private bool isCrouching = false;

    private void Awake()
    {
        cameraHolder = transform.Find("CameraHolder");
        if (cameraHolder == null)
        {
            Debug.LogError("CameraHolder not found");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Assumes the main camera is used for the player's view
        // cameraTransform = Camera.main.transform;

        originalY = cameraHolder.position.y;

    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // Handle head bobbing
        // HandleHeadBobbing();
        //Tiptoe
        Tiptoe();
        Crouch();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    // private void HandleHeadBobbing()
    // {
    //     if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
    //     {
    //         timer += bobbingSpeed;
    //         if (timer > Mathf.PI * 2)
    //         {
    //             timer -= Mathf.PI * 2;
    //         }

    //         float waveslice = Mathf.Sin(timer);
    //         float translateChange = waveslice * bobbingAmount;
    //         float totalAxes = Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput);
    //         totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
    //         translateChange *= totalAxes;

    //         Vector3 localPosition = cameraTransform.localPosition;
    //         localPosition.y = midpoint + translateChange;
    //         cameraTransform.localPosition = localPosition;
    //     }
    //     else
    //     {
    //         timer = 0.0f;
    //         Vector3 localPosition = cameraTransform.localPosition;
    //         localPosition.y = midpoint;
    //         cameraTransform.localPosition = localPosition;
    //     }
    // }
    
    // Method to check if the player is moving
    public bool IsMoving()
    {
        return Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0;
    }

    private void Tiptoe()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isTiptoeing)
        {
            Vector3 newPosition = cameraHolder.position;
            newPosition.y += 0.6f;
            cameraHolder.position = newPosition;
            isTiptoeing = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isTiptoeing)
        {
            Vector3 newPosition = cameraHolder.position;
            newPosition.y -= 0.6f; // Reset y position to originalY - 0.3f
            cameraHolder.position = newPosition;
            isTiptoeing = false;
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !isCrouching)
        {
            Vector3 newPosition = cameraHolder.position;
            newPosition.y -= 0.6f; // Adjust this value to control the crouch height
            cameraHolder.position = newPosition;
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
        {
            Vector3 newPosition = cameraHolder.position;
            newPosition.y += 0.6f; // Reset y position to originalY + 0.5f
            cameraHolder.position = newPosition;
            isCrouching = false;
        }
    }
}