using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 Position => transform.position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.AI.ArrivedAt(this);
        }
    }
}
