using System;
using UnityEngine;

namespace Character
{
    public class PlayerClimb : MonoBehaviour
    {
        
        [SerializeField] private int rayCasts = 25;
        [SerializeField] private float rayDistance = 1f;
        
        void FixedUpdate()
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;

            for (int i = 0; i < rayCasts; i++)
            {
                if (Physics.SphereCast(transform.position + (Vector3.up * (i * 0.1f)), 0f, transform.TransformDirection(Vector3.forward), out hit, rayDistance, layerMask))
                {
                    var distanceToObstacle = hit.distance;
                    Debug.Log(distanceToObstacle);
                    if (distanceToObstacle < 0.85f)
                    {
                        Debug.DrawRay(transform.position + (Vector3.up * (i * 0.1f)), transform.TransformDirection(Vector3.forward) * distanceToObstacle, Color.yellow);    
                    }
                    else
                    {
                        Debug.DrawRay(transform.position + (Vector3.up * (i * 0.1f)), transform.TransformDirection(Vector3.forward) * distanceToObstacle, Color.white);
                    }
                    
                }
            }
        }
    }
}
