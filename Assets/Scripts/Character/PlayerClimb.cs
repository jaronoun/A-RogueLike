using System;
using System.Collections;
using UnityEngine;
using Object = System.Object;

namespace Character
{
    public class PlayerClimb : MonoBehaviour
    {
        [Header("Physics")] 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private CapsuleCollider col;
        [SerializeField] private CapsuleCollider colBumper;

        [Header("Effectors")] 
        [SerializeField] private GameObject effectorEnable;
        [SerializeField] private GameObject effectorDisable;
        [SerializeField] private GameObject effectorIdle;

        [Header("Climb Settings")]
        [SerializeField] private float grabHeight = 2.25f;
        [SerializeField] private float grabDistance = 0.65f;
        [SerializeField] private float climbHeight = 1.5f;
        [SerializeField] private float climbDuration = 0.2f;

        private int rayCasts = 25;
        public bool isHanging = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            GameObject bumper = GameObject.Find("Bumper");
            colBumper = bumper.GetComponent<CapsuleCollider>();
            col = GetComponent<CapsuleCollider>();
            
            effectorEnable = Instantiate(effectorEnable, transform.position, Quaternion.identity);
            effectorEnable.SetActive(false);
            effectorDisable = Instantiate(effectorDisable, transform.position, Quaternion.identity);
            effectorDisable.SetActive(false);
            effectorIdle = Instantiate(effectorIdle, transform.position, Quaternion.identity);
            effectorIdle.SetActive(false);
        }

        void FixedUpdate()
        {
            // Debug.Log(isHanging);
            CanLedgeGrabVisuals();
        }
        
        public void Climb()
        {
            if (CheckLedge(out Vector3 hangPos, out Vector3 targetForward, out Vector3 hitTop))
            {
                StartCoroutine(SmoothTransition(hangPos, targetForward, 0.2f)); // Adjust duration as needed
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                isHanging = true;
            }
        }
        
        public void ClimbLedge()
        {
            if (CheckLedge(out Vector3 hangPos, out Vector3 targetForward, out Vector3 hitTop))
            {
                StartCoroutine(SmoothTransition(hitTop, targetForward, climbDuration));
            }
        }
        
        public void StopClimbing()
        {
            rb.useGravity = true;
            isHanging = false;
        }

        public bool CheckLedge(out Vector3 hangPos, out Vector3 targetForward, out Vector3 hitTop)
        {
            int layerMask = 1 << 3;
            layerMask = ~layerMask;
            
            RaycastHit hit;
            RaycastHit highestHit = new RaycastHit();
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 down = transform.TransformDirection(Vector3.down);
            Vector3 origin = transform.position;
            
            for (int i = 0; i < rayCasts; i++)
            {
                origin = transform.position + new Vector3(0, i * 0.1f, 0);
                if (Physics.SphereCast(origin, 0f, forward, out hit, grabDistance, layerMask))
                {
                    if (transform.position.y < hit.point.y)
                    {
                        highestHit = hit;
                    }
                }
            }
            if (highestHit.point != Vector3.zero)
            {
                if (Physics.SphereCast(highestHit.point + new Vector3(0, 1, 0), 0.05f, down, out hit, 1f, layerMask))
                {
                    // if (Physics.SphereCast(highestHit.point, 0f, Vector3.up, out hitUp, 1f, layerMask)) return false;
                    if (hit.point.y <= transform.position.y + grabHeight && !Physics.SphereCast(transform.position, 0.1f, Vector3.up, out var hitUp, 2.25f, layerMask))
                    {
                        hangPos = hit.point;
                        Vector3 offset = forward * -0.2f + transform.up * -1.75f;
                        hangPos += offset;
                        targetForward = -highestHit.normal;
                        hitTop = hit.point;
                        return true;
                    }
                }
            }
            hangPos = Vector3.zero;
            targetForward = Vector3.zero;
            hitTop = Vector3.zero;
            return false;
        }
        
        private IEnumerator SmoothTransition(Vector3 targetPosition, Vector3 targetForward, float duration)
        {
            colBumper.enabled = false;
            col.enabled = false;
            Vector3 startPosition = transform.position;
            Vector3 startForward = transform.forward;
            float time = 0;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                transform.forward = Vector3.Slerp(startForward, targetForward, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            // Ensure the final position and rotation are set
            transform.position = targetPosition;
            transform.forward = targetForward;
            colBumper.enabled = true;
            col.enabled = true;
        }

        
        public void CanLedgeGrabVisuals()
        {
            int layerMask = 1 << 3;
            layerMask = ~layerMask;

            RaycastHit hit = new RaycastHit();
            RaycastHit hitUp = new RaycastHit();
            RaycastHit highestHit = new RaycastHit();
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 down = transform.TransformDirection(Vector3.down);
            Vector3 up = transform.TransformDirection(Vector3.up);
            Vector3 origin = transform.position;

            for (int i = 0; i < rayCasts; i++)
            {
                origin = transform.position + new Vector3(0, i * 0.15f, 0);
                if (Physics.SphereCast(origin, 0.05f, forward, out hit, grabDistance, layerMask))
                {
                    Debug.DrawRay(origin, forward * hit.distance, Color.blue);
                    if (transform.position.y < hit.point.y)
                    {
                        highestHit.point = hit.point;
                    }
                }
            }

            if (highestHit.point != Vector3.zero)
            {
                effectorIdle.SetActive(true);
                effectorIdle.transform.position =
                    new Vector3(highestHit.point.x, transform.position.y + grabHeight, highestHit.point.z);
                if (Physics.SphereCast(highestHit.point + new Vector3(0, 1, 0), 0.05f, down, out hit, 1f, layerMask))
                {
                    Debug.DrawRay(highestHit.point + new Vector3(0, 1, 0), down, Color.red);
                    effectorDisable.SetActive(true);
                    effectorDisable.transform.position = hit.point;
                    Debug.DrawRay(transform.position, up * 2.25f, Color.green);
                    if (hit.point.y <= transform.position.y + grabHeight && !Physics.SphereCast(transform.position, 0.1f, Vector3.up, out hitUp, 2.25f, layerMask))
                    {
                        effectorEnable.SetActive(true);
                        effectorDisable.SetActive(false);
                        effectorIdle.SetActive(false);
                        effectorEnable.transform.position = hit.point;
                    }
                    else
                    {
                        effectorEnable.SetActive(false);
                    }
                }
            }
            else
            {
                effectorDisable.SetActive(false);
                effectorIdle.SetActive(false);
                effectorEnable.SetActive(false);
            }
        }
    }
}
    
    
