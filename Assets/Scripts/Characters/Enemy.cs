using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: SerializeField, Header("References")] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public EnemyAI AI { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [SerializeField] bool useRagdoll;

    [Header("Movement Settings")]
    [SerializeField] float WalkingSpeed = 1f;
    [SerializeField] float RunningSpeed = 2.5f;

    [Header("Debug")]
    [SerializeField] bool deathtTest;

    public bool HasTarget => AI.Target != null;
    public bool IsAlive => Health.Amount > 0;

    private void OnEnable()
    {
        Health.OnDeath += OnDeath;
        Health.OnDamaged += OnDamaged;
        ResetSpeed();
    }

    private void OnDisable()
    {
        Health.OnDeath -= OnDeath;
        Health.OnDamaged -= OnDamaged;
    }

    private void Start()
    {
        if (deathtTest)
        {
            Health.ApplyDamage(Health.Amount, null);
        }
    }

    private void Update()
    {
        Animator.SetFloat("Speed", animSpeed);
    }

    public void ResetSpeed()
    {
        NavMeshAgent.speed = HasTarget ? RunningSpeed : WalkingSpeed;
    }

    private float animSpeed
    {
        get
        {
            if (HasTarget) return (NavMeshAgent.velocity.magnitude / RunningSpeed) * 2;
            else return NavMeshAgent.velocity.magnitude / WalkingSpeed;

            /*if (NavMeshAgent.velocity.magnitude > 0) return HasTarget ? 2.0f : 1.0f;
            else return 0.0f;*/
        }
    }

    private void OnDeath()
    {
        if (useRagdoll)
        {
            Rigidbody.useGravity = false;
            Animator.StopPlayback();
            Animator.enabled = false;
            Collider.enabled = false;
            NavMeshAgent.isStopped = true;
            NavMeshAgent.enabled = false;
            Ragdoll.TurnOn();
        }
        else Animator.SetTrigger("Death");
    }

    private void OnDamaged(GameObject damageGiver)
    {
        if (damageGiver.TryGetComponent<Player>(out Player player)) AI.SetTarget(player);
        Animator.SetTrigger("Damaged");
    }
}
