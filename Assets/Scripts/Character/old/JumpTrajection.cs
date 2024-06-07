using System.Collections;
using UnityEngine;

namespace Character.old
{
    public class JumpTrajection : MonoBehaviour
    {

        [SerializeField] float initialVelocity;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] float step;
        [SerializeField] Transform jumpPoint;
        [SerializeField] float height;
        [SerializeField] Transform playerTransform;

        private void Update()
        {
            Vector3 playerForward = playerTransform.forward;
            Vector3 direction = playerForward;
            Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
            Vector3 targetPos = new Vector3(groundDirection.magnitude * 2, direction.y, 0); // Modify the distance by multiplying groundDirection.magnitude with a factor (e.g., 2)
            targetPos.z = 0;
            float height = this.height;
            float angle;
            float v0;
            float time;
            CalculatePathWithHeight(targetPos, height, out v0, out angle, out time);
            DrawPath(groundDirection.normalized, initialVelocity, angle, time, step);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                StartCoroutine(Coroutine_Movement(groundDirection.normalized, initialVelocity, angle, time));
            }
        }

        private void DrawPath(Vector3 direction, float v0, float angle, float time, float step)
        {
            step = Mathf.Max(0.01f, step);
            lineRenderer.positionCount = (int)(time / step) + 2;
            int count = 0;
            for (float t = 0; t < time; t += step)
            {
                float x = v0 * t * Mathf.Cos(angle);
                float y = v0 * t * Mathf.Sin(angle)- (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
                lineRenderer.SetPosition(count, jumpPoint.position + direction * x + Vector3.up * y);
                count++;
            }
            float xfinal = v0 * Mathf.Cos(angle * Mathf.Deg2Rad) * time;
            float yfinal = v0 * Mathf.Sin(angle * Mathf.Deg2Rad) * time - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);

            lineRenderer.SetPosition(count, jumpPoint.position + direction * xfinal + Vector3.up * yfinal);
        } 

        private float QuadraticEquation(float a, float b, float c, int sign)
        {
            float discriminant = Mathf.Pow(b, 2) - 4 * a * c;
            if (discriminant < 0) {
                discriminant = 0; // or handle error more appropriately
            }
            return (-b + sign * Mathf.Sqrt(discriminant)) / (2 * a);
        }

        private void CalculatePathWithHeight(Vector3 targetPos, float h, out float v0, out float angle, out float time)
        {
            float xt = targetPos.x;
            float yt = targetPos.y;
            float g = -Physics.gravity.y;

            float b = Mathf.Sqrt(2 * g * h);
            float a = (-0.5f * g);
            float c = -yt;

            float tplus = QuadraticEquation(a, b, c, 1);
            float tminus = QuadraticEquation(a, b, c, -1);
            time = tplus > tminus ? tplus : tminus;
            angle = Mathf.Atan(b * time / xt);
            v0 = b / Mathf.Sin(angle);
        }

        IEnumerator Coroutine_Movement(Vector3 direction, float v0, float angle, float time)
        {
            float t = 0;
            while (t < time)
            {
                float x = v0 * t * Mathf.Cos(angle);
                float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
                transform.position = jumpPoint.position + direction * x + Vector3.up * y;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
