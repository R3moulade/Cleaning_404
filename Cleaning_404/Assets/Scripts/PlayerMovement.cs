using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

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
    public float bobbingSpeed = 0.18f; // Speed of the bobbing
    public float bobbingAmount = 0.05f; // Amount of bobbing
    public float midpoint = 2.0f; // Default camera height

    private float timer = 0.0f;
    private Transform cameraTransform;

    private void Start()
    {
        rb = GetComponent <Rigidbody> ();
        rb.freezeRotation = true;

        readyToJump = true;

        // Assumes the main camera is used for the player's view
        cameraTransform = Camera.main.transform;
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
        HandleHeadBobbing();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
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
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
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

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void HandleHeadBobbing()
    {
        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            timer += bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }

            float waveslice = Mathf.Sin(timer);
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange *= totalAxes;

            Vector3 localPosition = cameraTransform.localPosition;
            localPosition.y = midpoint + translateChange;
            cameraTransform.localPosition = localPosition;
        }
        else
        {
            timer = 0.0f;
            Vector3 localPosition = cameraTransform.localPosition;
            localPosition.y = midpoint;
            cameraTransform.localPosition = localPosition;
        }
    }
}