using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject survivorPrefab;
    public GameObject enemyPrefab;

    [Header("Spawn Points (must be 4)")]
    public Transform[] spawnPoints = new Transform[4];

    public GameObject survivor { get; private set; }
    // Opposite spawn pairs
    private readonly int[] opposite = new int[] { 2, 3, 0, 1 };

    private void Start()
    {
        if (!ValidateSetup()) return;
        SpawnCharacters();
    }

    

    bool ValidateSetup()
    {
        bool ok = true;

        if (survivorPrefab == null)
        {
            Debug.LogError("[SpawnManager] Survivor prefab not assigned.");
            ok = false;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("[SpawnManager] Enemy prefab not assigned.");
            ok = false;
        }

        if (spawnPoints == null || spawnPoints.Length < 4)
        {
            Debug.LogError("[SpawnManager] Need 4 spawn points assigned.");
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

    void SpawnCharacters()
    {
        if (!ValidateSetup()) return;

        int enemyIndex = Random.Range(0, 4);
        int survivorIndex = opposite[enemyIndex];

        Vector3 sPos = spawnPoints[survivorIndex].position;
        Vector3 ePos = spawnPoints[enemyIndex].position;

        GameObject s = Instantiate(survivorPrefab, sPos, Quaternion.identity);
        s.active = true;
        survivor = s;
        GameObject e = Instantiate(enemyPrefab, ePos, Quaternion.identity);
        e.active = true;

        s.tag = "Survivor";
        e.tag = "Enemy";

        Debug.Log($"[SpawnManager] Spawned Survivor at {sPos} and Enemy at {ePos}");
    }

    void DestroyExistingCharacters()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Survivor"))
            Destroy(obj);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(obj);
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
                UnityEditor.Handles.Label(spawnPoints[i].position + Vector3.up * 0.3f, $"SP{i}");
            }
        }
    }
}
