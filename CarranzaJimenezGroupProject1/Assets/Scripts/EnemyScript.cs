using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public float detectionRange = 8.0f;
    public bool isHostile { get; private set; }
    public bool isBoss = false;
    public float spawnInterval;
    public int miniEnemySpawnCount;

    private Rigidbody2D enemyRb;
    private Transform player;
    private float nextSpawn;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
            playerObject = GameObject.Find("Player");

        if (playerObject != null)
            player = playerObject.transform;
    }

    private void FixedUpdate()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            isHostile = true;
            Vector2 direction = ((Vector2)player.position - enemyRb.position).normalized;
            enemyRb.linearVelocity = direction * speed;
        }
        else
        {
            isHostile = false;
            enemyRb.linearVelocity = Vector2.zero;
        }

        if (isBoss && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnInterval;
            //spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
        }

        if (transform.position.y < -10.0f)
            Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
