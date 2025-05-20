using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinAmount = 1;
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
            player.CoinHolder.AddAmount(coinAmount);
            Destroy(this.gameObject);
        }
    }
}
