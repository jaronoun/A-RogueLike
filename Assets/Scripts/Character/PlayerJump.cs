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

    private bool isGrounded;
    
    public bool CheckGrounded() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        return isGrounded;
    }

    public void Jump() {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}