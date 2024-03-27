using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float baseSpeed = 1.8f;
    [SerializeField] public float midSpeed = 3.6f;
    [SerializeField] public float maxSpeed = 5.4f;
    [SerializeField] public float rotationSpeed = 10.0f;
    [SerializeField] public float jumpForce = 5.0f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public PlayerControls controls; 
    public Transform cameraTransform;
    public PhysicMaterial slipperyMaterial; // Assign your slippery material in inspector
    public AnimationStateController animationStateController;

    private Rigidbody rb;
    private Vector2 move;
    private CapsuleCollider col;
    private PhysicMaterial defaultMaterial;

    private bool isRunning = false; // Flag to check if the player is currently running

    void Awake() {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => Jump();

        // Setup the Run action to trigger the methods for starting and stopping running
        controls.Gameplay.Run.performed += ctx => StartRunning();
        controls.Gameplay.Run.canceled += ctx => StopRunning();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Update() {
        
        // Update animator with grounded status
        if (IsGrounded()) {
            animationStateController.isGrounded();
        } else {
            animationStateController.isNotGrounded();
        }

        // Check if falling (optional, depending on your jump logic)
        if (!IsGrounded() && rb.velocity.y < 0 || rb.velocity.y > 0) { 
            animationStateController.isFalling();
            SetCapsuleColliderHitbox("mid air");
        } else {
            animationStateController.isNotFalling();
            SetCapsuleColliderHitbox("standing");
        }

        // If grounded, use the default material, else use the slippery material
        col.material = IsGrounded() ? defaultMaterial : slipperyMaterial;
    }

    void FixedUpdate() {

        float moveHorizontal = move.x;
        float moveVertical = move.y;

        // Calculate the direction vector based on camera orientation and joystick input
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0; // Ensure movement is only on the horizontal plane
        cameraRight.y = 0;
        Vector3 adjustedMovement = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        animationStateController.setWalkingAnim(moveHorizontal, moveVertical);

        // Trigger walking/running animations based on movement input
        if (adjustedMovement.magnitude > 0) {
            animationStateController.isWalking();
        } else {
            animationStateController.isNotWalking();
            animationStateController.isNotRunning();
            isRunning = false;
        }

        // Determine the current speed based on whether the player is running
        float currentSpeed = isRunning ? maxSpeed : Mathf.Lerp(baseSpeed, midSpeed, adjustedMovement.magnitude);

        // When the player isn't running, character should face the camera's direction
        if (!isRunning && adjustedMovement.magnitude > 0) {
            // Make the character face in the direction the camera is facing
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        } 
        // When running, the character maintains its direction without automatically aligning with the camera's aim
        else if (isRunning && adjustedMovement != Vector3.zero) {
            Quaternion runDirectionRotation = Quaternion.LookRotation(adjustedMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, runDirectionRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Apply the movement
        rb.MovePosition(rb.position + adjustedMovement * currentSpeed * Time.fixedDeltaTime);
    }

    private void StartRunning()
    {
        // Check if there's significant movement input
        float moveMagnitude = new Vector3(move.x, 0, move.y).magnitude;

        // Consider the player to be moving if the magnitude is greater than a small threshold (e.g., 0.1)
        if (moveMagnitude > 0.1f || moveMagnitude < -0.1f)
        {
            isRunning = true;
            animationStateController.isRunning();
        }
        else
        {
            // Optionally, stop running if there's no movement input
            isRunning = false;
            animationStateController.isNotRunning();
        }
    }

    private void StopRunning()
    {
        isRunning = false;
        animationStateController.isNotRunning();
    }

    void Jump() {
        if (IsGrounded()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animationStateController.StartJump();
        } 
    }

    public bool IsGrounded() {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }

    void SetCapsuleColliderHitbox(String colliderType) {
        switch (colliderType) {
            case "standing":
                col.center = new Vector3(0, 0.9f, 0);
                col.radius = 0.27f;
                col.height = 1.8f;
                break;
            case "mid air":
                col.center = new Vector3(0.1f, 1f, 0);
                col.radius = 0.2f;
                col.height = 1.312492f;
                break;
        }
    }

}
