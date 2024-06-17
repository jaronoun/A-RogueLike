using UnityEngine;

namespace Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] public float baseSpeed = 2.6f;
        [SerializeField] public float midSpeed = 3.8f;
        [SerializeField] public float maxSpeed = 5.4f;
        [SerializeField] public float rotationSpeed = 10.0f;

        [Header("Camera")]
        [SerializeField] private Transform cameraTransform;

        [Header("Physics")]
        [SerializeField] private Rigidbody rb;

        private Vector2 move;
        public Vector2 currentMove => move;

        private bool isRunning = false; // Flag to check if the player is currently running
        public bool isPlayerRunning => isRunning;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 movementInput) {
            move = movementInput;

            var moveHorizontal = move.x;
            var moveVertical = move.y;

            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            var adjustedMovement = (cameraForward * moveVertical + cameraRight * moveHorizontal);
            var currentSpeed = isRunning ? maxSpeed : Mathf.Lerp(baseSpeed, midSpeed, adjustedMovement.magnitude);

            if (adjustedMovement.magnitude > 0) {
                Quaternion targetRotation;
                if (isRunning) targetRotation = Quaternion.LookRotation(adjustedMovement);
                else targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
            rb.MovePosition(rb.position + adjustedMovement * (currentSpeed * Time.fixedDeltaTime));
        }

        public void StartRunning()
        {
            var moveMagnitude = new Vector3(move.x, 0, move.y).magnitude;
            if (moveMagnitude > 0.1f || moveMagnitude < -0.1f) isRunning = true;
            else isRunning = false;
        }

        public void StopRunning()
        {
            isRunning = false;
        }
    }
}
