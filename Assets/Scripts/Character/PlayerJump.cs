using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 4.5f;
    
    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    [Header("Physics")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider col;

    private bool isGrounded;
    
    public bool CheckGrounded() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        string colliderType = isGrounded ? "standing" : "mid air";
        SetCapsuleColliderHitbox(colliderType);
        return isGrounded;
    }

    public void Jump() {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void SetCapsuleColliderHitbox(string colliderType) {
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