using UnityEngine;

namespace Character
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 4.5f;
        [SerializeField] private float hangJumpForce = 3.5f;
    
        [Header("Ground Check")]
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;

        [Header("Physics")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private CapsuleCollider col;

        private bool isGrounded;
        private bool isJumping;
        public bool isPlayerGrounded => isGrounded;
        public bool isPlayerJumping => isJumping;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();
        }
    
        public bool CheckGrounded() {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            string colliderType = isGrounded ? "standing" : "mid air";
            SetCapsuleColliderHitbox(colliderType);
            return isGrounded;
        }

        public void Jump() {
            isJumping = true;
            if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        public void HangJump()
        {
            isJumping = true;
            rb.AddForce(Vector3.up * hangJumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        void SetCapsuleColliderHitbox(string colliderType) {
            switch (colliderType) {
                case "standing":
                    col.center = new Vector3(0, 0.9f, 0);
                    col.radius = 0.27f;
                    col.height = 1.8f;
                    // groundCheck.localPosition = new Vector3(0, 0, 0);
                    break;
                case "mid air":
                    col.center = new Vector3(0.1f, 1f, 0);
                    col.radius = 0.2f;
                    col.height = 1.312492f;
                    // groundCheck.localPosition = new Vector3(0, 0.4f, 0);
                    break;
            }
        }
    }
}