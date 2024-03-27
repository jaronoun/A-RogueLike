using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bumpForce = 10f; // The force to apply when bumping the character

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Check if the object entering the trigger has a Rigidbody (i.e., can be bumped)
        if (rb != null)
        {
            Vector3 bumpDirection = other.transform.position - transform.position;
            bumpDirection.y = 0; // Optionally, neutralize the vertical component if you only want horizontal bumping
            bumpDirection.Normalize(); // Get a normalized direction vector

            rb.AddForce(bumpDirection * bumpForce, ForceMode.VelocityChange); // Apply the bump force
        }
    }
}
