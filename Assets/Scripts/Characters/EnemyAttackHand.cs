using UnityEngine;

public class EnemyAttackHand : MonoBehaviour
{
    Enemy enemy;

    [field: SerializeField] public bool IsActive {  get; private set; }
    [SerializeField] GameObject impact;
    
    bool hasHit;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void Activate()
    {
        IsActive = true;
        hasHit = false;
    }

    public void Deactivate() { IsActive = false; }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsActive || hasHit || !enemy.IsAlive) return;

        if (other.TryGetComponent<Player>(out Player player) && player.IsAlive)
        {
            player.Health.ApplyDamage(enemy.Combat.AttackDamage);
            hasHit = true;

            if (impact != null)
            {
                Instantiate(impact, transform.position, 
                    Quaternion.FromToRotation(player.transform.position, enemy.transform.position));
            }
        }
    }
}
