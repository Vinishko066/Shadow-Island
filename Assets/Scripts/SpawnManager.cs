using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Existing Characters in Scene")]
    public GameObject survivor; // assign the existing Survivor object
    public GameObject enemy;    // assign the existing Enemy object

    [Header("Spawn Points (must be 4)")]
    public Transform[] spawnPoints = new Transform[4];

    // Opposite spawn pairs: 0->2, 1->3, 2->0, 3->1
    private readonly int[] opposite = new int[] { 2, 3, 0, 1 };

    private void Start()
    {
        if (!ValidateSetup()) return;
        MoveCharactersToSpawn();
    }

    bool ValidateSetup()
    {
        bool ok = true;

        if (survivor == null)
        {
            Debug.LogError("[SpawnManager] Survivor object not assigned.");
            ok = false;
        }

        if (enemy == null)
        {
            Debug.LogError("[SpawnManager] Enemy object not assigned.");
            ok = false;
        }

        if (spawnPoints == null || spawnPoints.Length < 4)
        {
            Debug.LogError("[SpawnManager] You must assign 4 spawn points.");
            ok = false;
        }
        else
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i] == null)
                {
                    Debug.LogError($"[SpawnManager] SpawnPoint {i} is missing.");
                    ok = false;
                }
            }
        }

        return ok;
    }

    void MoveCharactersToSpawn()
    {
        if (!ValidateSetup()) return;

        int enemyIndex = Random.Range(0, 4);
        int survivorIndex = opposite[enemyIndex];

        Vector3 survivorPos = spawnPoints[survivorIndex].position;
        Vector3 enemyPos = spawnPoints[enemyIndex].position;

        // Move the existing objects
        survivor.transform.position = survivorPos;
        survivor.SetActive(true); // just in case it was inactive
        enemy.transform.position = enemyPos;
        enemy.SetActive(true);

        // Assign tags if needed
        survivor.tag = "Survivor";
        enemy.tag = "Enemy";

        Debug.Log($"[SpawnManager] Survivor moved to {survivorPos}, Enemy moved to {enemyPos}");
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints == null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] != null)
            {
                Gizmos.DrawSphere(spawnPoints[i].position, 0.2f);
#if UNITY_EDITOR
                UnityEditor.Handles.Label(spawnPoints[i].position + Vector3.up * 0.3f, $"SP{i}");
#endif
            }
        }
    }
}
