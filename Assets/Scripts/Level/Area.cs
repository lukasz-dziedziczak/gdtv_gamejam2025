using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] EnemySpawner[] enemySpawners;
    [SerializeField] bool spawnOnStart = true;
    [SerializeField] Area[] respawnAreas;
    [SerializeField] AmmoSpawner[] ammoSpawners;
    [field: SerializeField] public bool HasPlayer { get; private set; }

    private void Start()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
        ammoSpawners = GetComponentsInChildren<AmmoSpawner>();

        if (spawnOnStart) SpawnAll();
    }


    public void SpawnAll()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.SpawnEnemy();
        }

        foreach (AmmoSpawner ammoSpawner in ammoSpawners)
        {
            ammoSpawner.SpawnAmmoPickup();
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
