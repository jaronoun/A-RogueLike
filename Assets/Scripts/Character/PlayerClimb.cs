using System;
using UnityEngine;
using Object = System.Object;

namespace Character
{
    public class PlayerClimb : MonoBehaviour
    {
        private int rayCasts = 20;
        private float rayDistance = 1f;
        [SerializeField] private GameObject effectorEnable;
        [SerializeField] private GameObject effectorDisable;
        [SerializeField] private GameObject effectorIdle;

        private void Start()
        {
            effectorEnable = Instantiate(effectorEnable, transform.position, Quaternion.identity);
            effectorEnable.SetActive(false);
            effectorDisable = Instantiate(effectorDisable, transform.position, Quaternion.identity);
            effectorDisable.SetActive(false);
            effectorIdle = Instantiate(effectorIdle, transform.position, Quaternion.identity);
            effectorIdle.SetActive(false);
        }

        void FixedUpdate()
        {
            int layerMask = 1 << 3;
            layerMask = ~layerMask;

            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 down = transform.TransformDirection(Vector3.down);
            Vector3 origin = transform.position;
            Vector3 highestHit = Vector3.zero;
            
            for (int i = 0; i < rayCasts; i++)
            { 
                origin = transform.position + new Vector3(0, i * 0.15f, 0);
                if (Physics.SphereCast(origin, 0.05f, forward, out hit, rayDistance, layerMask))
                {
                    Debug.DrawRay(origin, forward, Color.blue);
                    if (highestHit.y < hit.point.y)
                    {
                        highestHit = hit.point;
                    }
                }
            }
            if (highestHit != Vector3.zero)
            {
                effectorIdle.SetActive(true);
                effectorIdle.transform.position = highestHit;
                Debug.DrawRay(highestHit + new Vector3(0, 1, 0), down, Color.red);
                if (Physics.SphereCast(highestHit + new Vector3(0, 1, 0), 0.05f, down, out hit, 1f, layerMask))
                {
                    effectorDisable.SetActive(true);
                    effectorDisable.transform.position = hit.point;
                }
            }
            else
            {
                effectorDisable.SetActive(false);
                effectorIdle.SetActive(false);
            }
        }
    }
}
