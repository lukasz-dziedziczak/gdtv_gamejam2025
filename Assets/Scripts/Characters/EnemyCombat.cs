using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] EnemyAttackHand attackHand;
    [SerializeField] float attackRate;
    [SerializeField] float attackRateVariation = 0.1f;
    [field: SerializeField] public float AttackDamage = 20;
    [field: SerializeField] public bool IsAttacking;
    float timer;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    public bool CanAttack => /*!IsAttacking &&*/ timer <= 0;

    public void BeginAttack()
    {
        if (!CanAttack) return;
        IsAttacking = true;
        timer = attackRate + Random.Range(-attackRateVariation, attackRateVariation);
        enemy.Animator.SetTrigger("Attack");
    }

    public void AnimAttackStart()
    {
        if (attackHand != null)
            attackHand.Activate();
    }

    public void AnimAttackEnd()
    {
        if (attackHand != null)
            attackHand.Deactivate();
    }

    public void AttackAnimationComplete()
    {
        IsAttacking = false;
    }
}
