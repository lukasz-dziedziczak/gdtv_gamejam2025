using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 target;
    float speed;
    float lifetime;
    float damageAmount;
    Health health;
    [SerializeField] GameObject impact;
    GameObject damageGiver;

    public struct ProjectilePayload
    {
        public Vector3 Target;
        public float Speed;
        public float Damage;
        public Health Health;
        public GameObject DamageGiver;
    }

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
                if (health != null) health.ApplyDamage(damageAmount, damageGiver);
                Destroy(gameObject);
            }
        }
    }

    public void Initilize(ProjectilePayload payload)
    {
        target = payload.Target;
        speed = payload.Speed;
        damageAmount = payload.Damage;
        health = payload.Health;
        damageGiver = payload.DamageGiver;
        lifetime = DistanceToTarget / speed;
        transform.LookAt(target);
    }

    private float DistanceToTarget => Vector3.Distance(transform.position, target);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            if (impact != null) Instantiate(impact, transform.position, transform.rotation);
            health.ApplyDamage(damageAmount, damageGiver);
            Destroy(gameObject);
        }
    }
}
