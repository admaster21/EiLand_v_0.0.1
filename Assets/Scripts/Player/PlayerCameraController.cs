using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public Transform cameraPivot;
    public Camera mainCamera;

    [Header("Camera View")]
    public float cameraSmoothTime = 0.22f;
    public Vector3 firstPersonOffset = new Vector3(0f, 0f, 0f);
    public Vector3 thirdPersonOffset = new Vector3(0f, 1.2f, -4f);
    public LayerMask cameraCollisionLayers;
    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    
    
    private float yaw = 0f;
    private float pitch = 0f;

    private bool isFirstPerson = true;

    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 targetCameraOffset;

    public Transform playerBody;

    // Runs once at the start to set up reference and initialize the camera view.
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetCameraOffset = firstPersonOffset;
        UpdateCameraView();
    }

    // Handles frame-based input, camera updates, and grounded checks.
    void Update()
    {
        HandleMouseLook();
        HandleCameraToggle();
        SmoothCameraTransition();
    }

    // Rotates the player horizontally and the camera vertically based on mouse movement.
    void HandleMouseLook()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
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
        Vector3 desiredLocalPosition = targetCameraOffset;
        Vector3 pivotWorldPosition = cameraPivot.position;
        Vector3 desiredWorldPosition = cameraPivot.TransformPoint(desiredLocalPosition);

        Vector3 direction = (desiredWorldPosition - pivotWorldPosition).normalized;
        float distance = Vector3.Distance(pivotWorldPosition, desiredWorldPosition);

        Vector3 finalWorldPosition = desiredWorldPosition;

        if (Physics.SphereCast(
            pivotWorldPosition,
            cameraCollisionRadius,
            direction,
            out RaycastHit hit,
            distance,
            cameraCollisionLayers))
        {
            finalWorldPosition = hit.point - direction * cameraCollisionOffset;
        }

        Vector3 finalLocalPosition = cameraPivot.InverseTransformPoint(finalWorldPosition);

        mainCamera.transform.localPosition = Vector3.SmoothDamp(
            mainCamera.transform.localPosition,
            finalLocalPosition,
            ref cameraVelocity,
            cameraSmoothTime
        );
    }
}
