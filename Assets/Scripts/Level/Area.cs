using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] EnemySpawner[] enemySpawners;
    [SerializeField] bool spawnOnStart = true;
    [SerializeField] Area[] respawnAreas;
    [field: SerializeField] public bool HasPlayer { get; private set; }

    private void Start()
    {
        if (spawnOnStart) SpawnAll();
    }


    public void SpawnAll()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            HasPlayer = true;

            foreach (Area area in respawnAreas)
            {
                area.SpawnAll();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player)) HasPlayer = false;
    }
}
