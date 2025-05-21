using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Waypoint[] waypoints;

    Enemy lastSpawned;
    Enemy cleanUp;

    public Enemy SpawnEnemy()
    {
        if (lastSpawned != null) return lastSpawned;
        Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.AI.SetWaypoints(waypoints);
        lastSpawned = enemy;
        lastSpawned.Health.OnDeath += OnLastSpawnedDeath;
        if (cleanUp != null)
        {
            Destroy(cleanUp.gameObject);
            cleanUp = null;
        }
        return enemy;
    }

    public void SetWaypoints(Waypoint[] newWaypoints) { waypoints = newWaypoints; }

    private void OnLastSpawnedDeath()
    {
        lastSpawned.Health.OnDeath -= OnLastSpawnedDeath;
        cleanUp = lastSpawned;
        lastSpawned = null;
    }
}
