using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public Transform cameraTransform;
    public Transform characterTransform;
    private Vector2 look;

    public PlayerControls controls;

    public Rig headRig;

    public LayerMask aimLayerMask; // The layer mask to use for the aim raycast
    public Transform headTransform; // Assign this to your character's head transform in the Inspector
    public Transform headTarget;

    public float rayLength = 100f; // How far the raycast will go

    void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Update() {

        AdjustHeadRigWeight();
        
        ShootRaycastFromCamera();

        if (thirdPersonCamera) {
            thirdPersonCamera.m_XAxis.Value += look.x * Time.deltaTime * 200;
            thirdPersonCamera.m_YAxis.Value += look.y * Time.deltaTime;
        }
    }

    void ShootRaycastFromCamera()
    {
        Vector3 forwardDirection = cameraTransform.forward; // Use the camera's forward direction
        RaycastHit hit;

        // Perform the raycast from the camera's position, forward
        if (Physics.Raycast(cameraTransform.position, forwardDirection, out hit, rayLength, aimLayerMask))
        {
            // If the ray hits an object, position the headTarget at the hit point
            headTarget.position = hit.point;
            // Optionally, draw a red line from the camera to the hit point for debugging
            Debug.DrawLine(cameraTransform.position, hit.point, Color.red);
        }
        else
        {
            // If nothing is hit, position the headTarget in the direction the camera is aiming, at the maximum ray length
            headTarget.position = cameraTransform.position + forwardDirection * rayLength;
            // Optionally, draw a green line in the direction of the raycast for debugging
            Debug.DrawLine(cameraTransform.position, cameraTransform.position + forwardDirection * rayLength, Color.green);
        }
    }

    void AdjustHeadRigWeight()
    {
        // Calculate the relative yaw angle between the character and the camera
        Vector3 characterForwardFlat = Vector3.ProjectOnPlane(characterTransform.forward, Vector3.up);
        Vector3 cameraForwardFlat = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        float angle = Vector3.SignedAngle(characterForwardFlat, cameraForwardFlat, Vector3.up);

        // Normalize the angle to be within [0, 180] range
        float absAngle = Mathf.Abs(angle);

        // Determine the weight based on the angle
        float weight;
        if (absAngle > 90f) {
            if (absAngle >= 180f) {
                weight = 0f; // Set weight to 0 when angle is 180 degrees
            } else {
                // Interpolate weight to decrease from 0.5 to 0 between 90 and 180 degrees
                weight = Mathf.Lerp(1f, 0f, (absAngle - 90f) / 90f);
            }
        } else {
            weight = 1f; // Full weight when angle is less than or equal to 90 degrees
        }

        // Apply the calculated weight to the head rig
        headRig.weight = weight;
    }

}
