using UnityEngine;

public class DeathTest : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] float time;

    float lifeTime;

    private void Start()
    {
        lifeTime = time;

    }

    private void Update()
    {
        if (health != null && health.Amount > 0)
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
            {
                health.ApplyDamage(health.Amount);
            }
        }
    }
}
