using System;
using UnityEngine;
using Object = System.Object;

namespace Character
{
    public class PlayerClimb : MonoBehaviour
    {
        [Header("Rigidbody")] 
        [SerializeField] private Rigidbody rb;

        [Header("Effectors")] 
        [SerializeField] private GameObject effectorEnable;
        [SerializeField] private GameObject effectorDisable;
        [SerializeField] private GameObject effectorIdle;

        [Header("Climb Settings")] [SerializeField]
        private float grabHeight = 1.5f;

        [SerializeField] private float grabDistance = 1f;

        private int rayCasts = 25;
        public bool isHanging = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
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
            if (CanLedgeGrab())
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                isHanging = true;
            }
        }
        
        public void StopClimbing()
        {
            rb.useGravity = true;
            isHanging = false;
        }

        public bool CanLedgeGrab()
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
                    if (hit.point.y <= transform.position.y + grabHeight)
                    {
                        Vector3 hangPos = hit.point;
                        Vector3 offset = forward * -0.2f + transform.up * -1.75f;
                        hangPos += offset;
                        transform.position = hangPos;
                        transform.forward = -highestHit.normal;
                        return true;
                    }
                }
            }
            return false;
        }

        public void CanLedgeGrabVisuals()
        {
            int layerMask = 1 << 3;
            layerMask = ~layerMask;

            RaycastHit hit = new RaycastHit();
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 down = transform.TransformDirection(Vector3.down);
            Vector3 origin = transform.position;
            Vector3 highestHit = Vector3.zero;

            for (int i = 0; i < rayCasts; i++)
            {
                origin = transform.position + new Vector3(0, i * 0.15f, 0);
                if (Physics.SphereCast(origin, 0.05f, forward, out hit, grabDistance, layerMask))
                {
                    Debug.DrawRay(origin, forward * hit.distance, Color.blue);
                    if (transform.position.y < hit.point.y)
                    {
                        highestHit = hit.point;
                    }
                }
            }

            if (highestHit != Vector3.zero)
            {
                effectorIdle.SetActive(true);
                effectorIdle.transform.position =
                    new Vector3(highestHit.x, transform.position.y + grabHeight, highestHit.z);
                if (Physics.SphereCast(highestHit + new Vector3(0, 1, 0), 0.05f, down, out hit, 1f, layerMask))
                {
                    Debug.DrawRay(highestHit + new Vector3(0, 1, 0), down, Color.red);
                    effectorDisable.SetActive(true);
                    effectorDisable.transform.position = hit.point;
                    if (hit.point.y <= transform.position.y + grabHeight)
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
    
    
