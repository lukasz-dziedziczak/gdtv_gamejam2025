using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 target;
    float speed;
    float lifetime;
    [SerializeField] GameObject impact;

    private void Update()
    {
        if (speed > 0)
        {
            Vector3 pos = transform.position;
            pos += transform.forward * Time.deltaTime * speed;
            transform.position = pos;

            lifetime -= Time.deltaTime;

            if (lifetime <= 0)
            {
                if (impact != null) Instantiate(impact, target, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void Initilize(Vector3 target, float speed)
    {
        this.target = target;
        this.speed = speed;
        lifetime = DistanceToTarget / speed;
        transform.LookAt(target);
    }

    private float DistanceToTarget => Vector3.Distance(transform.position, target);
}
