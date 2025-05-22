using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] AmmoPickup ammoPrefab;
    AmmoPickup ammoPickup;

    public void SpawnAmmoPickup()
    {
        if (ammoPickup != null) return;
        ammoPickup = Instantiate(ammoPrefab, transform.position, transform.rotation);
    }
}
