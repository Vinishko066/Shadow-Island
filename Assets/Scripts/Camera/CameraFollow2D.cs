using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Transform target;       // Object to follow
    public float smoothSpeed = 5f; // Camera follow speed
    public Vector3 offset;         // Offset from target, e.g., (0,0,-10)
    public SpawnManager spawnManager; // assign in inspector

    void Start()
    {
        if (target == null)
        {
            if (spawnManager != null && spawnManager.survivor != null)
            {
                target = spawnManager.survivor.transform;
            }
            else
            {
                GameObject player = GameObject.FindGameObjectWithTag("Survivor");
                if (player != null) target = player.transform;
            }
        }
    }


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
