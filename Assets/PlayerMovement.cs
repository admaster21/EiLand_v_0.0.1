using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float crouchMultiplier = 0.5f;
    public float jumpForce = 5f;
    public float airControlMultiplier = 0.35f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public Transform cameraPivot;
    public Camera mainCamera;

    [Header("Camera View")]
    public float cameraSmoothTime = 0.15f;
    public Vector3 firstPersonOffset = new Vector3(0f, 0f, 0f);
    public Vector3 thirdPersonOffset = new Vector3(0f, 1.2f, -4f);

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public LayerMask groundLayers;
    public float groundCheckDistance = 0.25f;

    private Rigidbody rb;

    private float yaw = 0f;
    private float pitch = 0f;

    private bool isGrounded = true;
    private bool isFirstPerson = true;
    private bool touchingWall = false;

    private Vector3 wallNormal;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 targetCameraOffset;

    // Runs once at the start to set up references and initialize camera state.
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetCameraOffset = firstPersonOffset;
        UpdateCameraView();
    }

    // Handles frame-based input, camera updates, and grounded checks.
    void Update()
    {
        CheckGround();
        HandleMouseLook();
        HandleJump();
        HandleCameraToggle();
        SmoothCameraTransition();
    }

    // Handles physics-based movement at a fixed time step.
    void FixedUpdate()
    {
        touchingWall = false;
        HandleMovement();
    }

    // Rotates the player horizontally and the camera vertically based on mouse movement.
    void HandleMouseLook()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    // Moves the player using Rigidbody velocity and reduces control while airborne.
    void HandleMovement()
    {
        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed *= sprintMultiplier;

        if (Input.GetKey(KeyCode.LeftControl))
            currentSpeed *= crouchMultiplier;

        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        Vector3 velocity = transform.TransformDirection(moveDirection) * currentSpeed;

        if (!isGrounded)
        {
            velocity *= airControlMultiplier;

            if (touchingWall)
                velocity = Vector3.ProjectOnPlane(velocity, wallNormal);
        }

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    // Makes the player jump only when grounded.
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Switches between first-person and third-person camera views.
    void HandleCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            UpdateCameraView();
        }
    }

    // Updates the target camera offset based on the active camera mode.
    void UpdateCameraView()
    {
        targetCameraOffset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;
    }

    // Smoothly moves the camera toward the current target offset.
    void SmoothCameraTransition()
    {
        mainCamera.transform.localPosition = Vector3.SmoothDamp(
            mainCamera.transform.localPosition,
            targetCameraOffset,
            ref cameraVelocity,
            cameraSmoothTime
        );
    }

    // Checks whether the player is standing on valid ground using a raycast.
    void CheckGround()
    {
        isGrounded = Physics.Raycast(
            groundCheckPoint.position,
            Vector3.down,
            groundCheckDistance,
            groundLayers
        );
    }

    // Detects wall contact while airborne so movement can
    // slide along the wall instead of climbing it.
    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y < 0.2f)
            {
                touchingWall = true;
                wallNormal = contact.normal;
                return;
            }
        }
    }

    // Clears wall contact when the player stops touching the wall.
    void OnCollisionExit(Collision collision)
    {
        touchingWall = false;
    }
}