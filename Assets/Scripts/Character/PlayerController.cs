using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody rb;
    private CapsuleCollider col;
    
    [Header("Controls")]
    [SerializeField] private PlayerControls controls;

    [Header("Camera")]
    [SerializeField] private PlayerCamera playerCamera;

    [Header("Movement")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Jumping")]
    [SerializeField] private PlayerJump playerJump;

    [Header("Physics")]
    [SerializeField] private PhysicMaterial slipperyMaterial; // Assign your slippery material in inspector
    private PhysicMaterial defaultMaterial;

    private Vector2 move;
    private Vector2 look;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        controls = new PlayerControls();

        // Controls for moving
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        // Controls for jumping
        controls.Gameplay.Jump.performed += ctx => Jump();
        // Controls for running
        controls.Gameplay.Run.performed += ctx => StartRunning();
        controls.Gameplay.Run.canceled += ctx => StopRunning();
        // Controls for looking
        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }

    private void Update() {
        playerCamera.Look(look);
        SetMaterial();
        // Call other non-movement methods here (e.g., ShootRaycastFromCamera, AdjustHeadRigWeight)
    }

    private void FixedUpdate() {
        playerMovement.Move(move);
        // Call other non-movement methods here (e.g., Jump, Run)
    }

    private void Jump() {
        playerJump.Jump();
    }

    private void StartRunning() {
        playerMovement.StartRunning();
    }

    private void StopRunning() {
        playerMovement.StopRunning();
    }

    private void SetMaterial() {
        // If grounded, use the default material, else use the slippery material
        col.material = playerJump.CheckGrounded() ? defaultMaterial : slipperyMaterial;
    }
}