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
    public Animator playerAnimator;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public LayerMask groundLayers;
    public float groundCheckDistance = 0.25f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool touchingWall = false;
    private Vector3 wallNormal;
    

    // Runs once at the start to set up references and initialize camera state.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Handles frame-based input, camera updates, and grounded checks.
    void Update()
    {
        CheckGround();
        HandleJump();
    }

    // Handles physics-based movement at a fixed time step.
    void FixedUpdate()
    {
        touchingWall = false;
        HandleMovement();
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

        float animationSpeed = moveDirection.magnitude;

        if (Input.GetKey(KeyCode.LeftShift) && animationSpeed > 0f)
        {
            animationSpeed = 1.5f;
        }
        playerAnimator.SetFloat("MoveSpeed", animationSpeed);
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

            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Jump");
            }

            isGrounded = false;
        }
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