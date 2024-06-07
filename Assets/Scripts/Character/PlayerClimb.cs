using UnityEngine;

namespace Character
{
    public class PlayerClimb : MonoBehaviour
    {}
}
    //     [SerializeField] private LayerMask climbableLayer; // Layer to identify climbable surfaces
    //     [SerializeField] private Transform cameraTransform;
    //     [SerializeField] private Rigidbody rb;
    //     [SerializeField] private Animator animator; // Assume an Animator component is managing animations

    //     [Header("Climbing and Hanging")]
    //     [SerializeField] private float climbSpeed = 3f;
    //     [SerializeField] private float checkDistance = 0.5f;
    //     [SerializeField] private bool isClimbing = false;
    //     [SerializeField] private bool isHanging = false;

    //     void Update()
    //     {
    //         if (isClimbing)
    //         {
    //             // ClimbMovement();
    //         }

    //         if (isHanging)
    //         {
    //             CheckForClimbOrDrop();
    //         }

    //         CheckForClimbableOrHangable();
    //     }

    //     private void CheckForClimbableOrHangable()
    //     {
    //         RaycastHit hit;
    //         if (Physics.Raycast(transform.position + Vector3.up * 2, transform.forward, out hit, checkDistance, climbableLayer))
    //         {
    //             if (Input.GetKeyDown(KeyCode.E) && !isClimbing && !isHanging) // Toggle climbing on/off
    //             {
    //                 if (hit.normal.y > 0.9f) // Assuming a nearly horizontal surface above the player indicates a ledge
    //                 {
    //                     StartHanging(hit.point);
    //                 }
    //                 else
    //                 {
    //                     StartClimbing();
    //                 }
    //             }
    //         }
    //         else if (isClimbing)
    //         {
    //             StopClimbing();
    //         }
    //     }

    //     private void StartClimbing()
    //     {
    //         isClimbing = true;
    //         isHanging = false;
    //         rb.isKinematic = true;
    //         rb.useGravity = false;
    //         // animator.SetBool("IsClimbing", true);
    //     }

    //     private void StartHanging(Vector3 hangPoint)
    //     {
    //         isHanging = true;
    //         isClimbing = false;
    //         rb.isKinematic = true;
    //         rb.useGravity = false;
    //         transform.position = hangPoint + (transform.forward * -0.5f); // Adjust position to hang at the edge
    //         // animator.SetBool("IsHanging", true);
    //     }

    //     // private void ClimbMovement()
    //     // {
    //     //     float verticalInput = Input.GetAxis("Vertical");
    //     //     transform.Translate(Vector3.up * (verticalInput * climbSpeed * Time.deltaTime));
    //     // }

    //     // private void CheckForClimbOrDrop()
    //     // {
    //     //     if (Input.GetKeyDown(KeyCode.Space)) // Assuming Space to climb up from hanging
    //     //     {
    //     //         StartClimbing();
    //     //     }
    //     //     else if (Input.GetKeyDown(KeyCode.C)) // Assuming C to drop down from hanging
    //     //     {
    //     //         StopClimbing();
    //     //     }
    //     // }

    //     private void StopClimbing()
    //     {
    //         isClimbing = false;
    //         isHanging = false;
    //         rb.isKinematic = false;
    //         rb.useGravity = true;
    //     }
    // }
// }
