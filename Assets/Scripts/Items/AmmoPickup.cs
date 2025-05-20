using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 50;
    [SerializeField] Transform mesh;
    [SerializeField] float rotationSpeed;

    private void Update()
    {
        Vector3 rot = mesh.eulerAngles;
        rot.y += rotationSpeed * Time.deltaTime;
        mesh.eulerAngles = rot;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Shooting.AddToAmmoStorage(ammoAmount);
            Destroy(this.gameObject);
        }
    }
}
