using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform[] spawnPoints;
    public int npcCount = 3;
    public bool spawnOnStart = true;
    public float spawnRadius = 5.0f;

    private void Start()
    {
        if (spawnOnStart)
            SpawnNPCs();
    }

    public void SpawnNPCs()
    {
        if (npcPrefab == null)
        {
            Debug.LogError("NPCSpawner needs an NPC prefab assigned.", this);
            return;
        }

        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int amountToSpawn = Mathf.Min(npcCount, spawnPoints.Length);
            for (int i = 0; i < amountToSpawn; i++)
                Instantiate(npcPrefab, spawnPoints[i].position, spawnPoints[i].rotation);

            return;
        }

        for (int i = 0; i < npcCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0.0f);
            Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
