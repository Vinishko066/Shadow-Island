using UnityEngine;
using UnityEngine.InputSystem;

public class LogCollector : MonoBehaviour
{
    [Header("Carry Settings")]
    public Transform carryPoint;
    public float pickupRange = 5f;

    [Header("Dock Settings")]
    public Transform dock;        // Assign your dock in inspector
    public float dockRange = 3f;  // Distance needed to place log

    private GameObject carriedLog = null;
    private PlayerControls controls;

    [Header("Dock Tracking")]
    public int logsPlaced = 0;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.PickUp.performed += ctx => HandleInteract();
    }

    private void OnEnable() => controls.Gameplay.Enable();
    private void OnDisable() => controls.Gameplay.Disable();

    void HandleInteract()
    {
        if (carriedLog == null)
        {
            TryPickupLog();
        }
        else
        {
            float distanceToDock = Vector2.Distance(transform.position, dock.position);
            Debug.Log($"[LogCollector] Carrying log. Distance to dock: {distanceToDock}");

            if (distanceToDock <= dockRange)
            {
                Debug.Log("[LogCollector] Within range to place log at dock.");
                PlaceLogAtDock();
            }
            else
            {
                Debug.Log("[LogCollector] Carrying log but too far from dock.");
            }
        }
    }

    void TryPickupLog()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Log"))
            {
                PickUp(hit.gameObject);
                return;
            }
        }

        Debug.Log("[LogCollector] No logs in range to pick up.");
    }

    void PickUp(GameObject log)
    {
        carriedLog = log;

        carriedLog.transform.SetParent(carryPoint);
        carriedLog.transform.localPosition = Vector3.zero;
        carriedLog.transform.localRotation = Quaternion.identity;

        Rigidbody2D rb = carriedLog.GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        Collider2D col = carriedLog.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Debug.Log("[LogCollector] Picked up log: " + log.name);
    }

    void PlaceLogAtDock()
    {
        carriedLog.transform.SetParent(null);
        carriedLog.transform.position = dock.position;
        carriedLog.transform.localRotation = Quaternion.identity;

        Rigidbody2D rb = carriedLog.GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = false;

        Collider2D col = carriedLog.GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        carriedLog = null;
        logsPlaced++;

        Debug.Log("[LogCollector] Log placed at dock. Total logs placed: " + logsPlaced);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);

        if (dock != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(dock.position, dockRange);
        }
    }
}
