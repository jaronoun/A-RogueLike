using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Character.old
{
    public class IKFootPlacement : MonoBehaviour
    {
        [SerializeField] public Transform leftFoot;
        [SerializeField] public Transform rightFoot;
        [SerializeField] private Transform leftFootTarget;
        [SerializeField] private Transform rightFootTarget;
        [SerializeField] private TwoBoneIKConstraint leftFootIK;
        [SerializeField] private TwoBoneIKConstraint rightFootIK;
        [SerializeField] private MultiRotationConstraint leftFootRotation;
        [SerializeField] private MultiRotationConstraint rightFootRotation;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float distanceToGround = 1f;
        [SerializeField] private float footOffset = 0.1f; // Raise the foot a bit above the ground
    
        private void Update()
        {
            AdjustFootToGround(leftFootIK, leftFootTarget);
            AdjustFootToGround(rightFootIK, rightFootTarget);
        }

        private void AdjustFootToGround(TwoBoneIKConstraint footIk, Transform footTarget)
        {
            // The layer mask should ideally be set to detect only the layers that the ground belongs to
            LayerMask groundLayerMask = groundLayer;

            // Raycast starts from the foot's position on the ground and goes upwards
            Vector3 rayStart = footTarget.position;
            Vector3 rayDirection = Vector3.up; // Shooting the ray upwards

            float rayLength = distanceToGround + 1f; // Length of the ray

            // Shoot a raycast upwards
            RaycastHit hit;
            if (Physics.Raycast(rayStart, rayDirection, out hit, rayLength, groundLayerMask))
            {
                // Logic for when the raycast hits an object above the foot
                // This could adjust footIK based on your specific needs or provide info about the object above

                // For demonstration, let's adjust the target position to just below the hit point as an example
                // Note: You'll likely want different logic here based on your game's requirements
                footIk.data.target.position = hit.point - Vector3.up * footOffset;
            }

            // Visualize the ray in the scene view
            Debug.DrawRay(rayStart, rayDirection * rayLength, Color.green);
        }


    }
}
