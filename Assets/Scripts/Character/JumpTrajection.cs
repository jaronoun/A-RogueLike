using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrajection : MonoBehaviour
{
    IEnumerator JumpRoutine()
    {
        float time = 0;
        float jumpHeight = 1.5f;
        float jumpDuration = 0.5f;
        Vector3 startPos = transform.position;
        Vector3 jumpVector = Vector3.up * jumpHeight;
        while (time < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPos, startPos + jumpVector, time / jumpDuration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos + jumpVector;
    }
}
