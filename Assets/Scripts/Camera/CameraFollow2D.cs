using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;       // Object to follow
    public float smoothSpeed = 5f; // Camera follow speed
    public Vector3 offset;         // Offset from target, e.g., (0,0,-10)

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position with offset
        Vector3 desiredPosition = target.position + offset;

        // Smooth interpolation
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Keep camera's z unchanged for 2D
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

    // Change target dynamically
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
