using UnityEngine;

public class EnemyAttackHand : MonoBehaviour
{
    Enemy enemy;

    [field: SerializeField] public bool IsActive {  get; private set; }
    
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
        if (hasHit) return;

        if (other.TryGetComponent<Player>(out Player player))
        {
            player.Health.ApplyDamage(enemy.Combat.AttackDamage);
            hasHit = true;
        }
    }
}
