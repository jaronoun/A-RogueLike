using Character.PlayerStateMachine;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player")]
        private Rigidbody rb;
        private CapsuleCollider col;
    
        [Header("Controls")]
        [SerializeField] private PlayerInput input;

        [Header("Camera")]
        [SerializeField] private PlayerCamera playerCamera;

        [Header("Movement")]
        [SerializeField] private PlayerMovement playerMovement;

        [Header("Jumping")]
        [SerializeField] private PlayerJump playerJump;
        
        [Header("Climbing")] 
        [SerializeField] private PlayerClimb playerClimb;

        [Header("State Manager")]
        [SerializeField] private PlayerStateManager playerStateManager;

        [Header("Physics")]
        [SerializeField] private PhysicMaterial slipperyMaterial; // Assign your slippery material in inspector
        private PhysicMaterial defaultMaterial;
    
        private Vector2 move;
        private Vector2 look;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();

            input = new PlayerInput();
            // Input for moving
            input.Gameplay.Move.performed += ctx => { 
                move = ctx.ReadValue<Vector2>(); 
                playerStateManager.HandleMovement(move, playerMovement.isPlayerRunning); 
            };
            input.Gameplay.Move.canceled += ctx => { 
                move = Vector2.zero; 
                playerStateManager.HandleMovement(move, playerMovement.isPlayerRunning);
            };
            // Input for jumping
            input.Gameplay.Jump.performed += ctx => { 
                Jump(); 
                playerStateManager.HandleJump(playerJump.isPlayerGrounded); 
            };
            
            input.Gameplay.Climb.performed += ctx => { 
                Climb(); 
            };
            // Input for running
            input.Gameplay.Run.performed += ctx => { 
                StartRunning(); 
                playerStateManager.HandleMovement(move, playerMovement.isPlayerRunning); 
            };
            input.Gameplay.Run.canceled += ctx => { 
                StopRunning(); 
                playerStateManager.HandleMovement(move, playerMovement.isPlayerRunning);
            };
            // Input for looking
            input.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
            input.Gameplay.Look.canceled += ctx => look = Vector2.zero;
        }

        private void OnEnable() {
            input.Gameplay.Enable();
        }

        private void OnDisable() {
            input.Gameplay.Disable();
        }

        private void Update() {
            playerCamera.Look(look);
            playerCamera.ShootRaycastFromCamera();
            playerCamera.AdjustHeadRigWeight();
            SetMaterial();

        }

        private void FixedUpdate()
        {
            if (playerClimb.isHanging) return;
            playerMovement.Move(move);
        }

        private void Jump() {
            if (playerClimb.isHanging)
            {
                // playerJump.HangJump();
                playerClimb.ClimbLedge();
                playerStateManager.HandleLedgeClimb(playerClimb.isHanging);
                return;
            }
            playerJump.Jump();
        }
        
        private void Climb() {
            if (!playerClimb.isHanging)
            {
                playerClimb.Climb();
            }
            else
            {
                playerClimb.StopClimbing();
            }
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
}