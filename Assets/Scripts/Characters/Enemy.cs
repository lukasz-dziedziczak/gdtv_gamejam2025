using System;
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
    [field: SerializeField] public EnemyCombat Combat { get; private set; }

    [SerializeField] bool useRagdoll;

    [Header("Movement Settings")]
    [SerializeField] float WalkingSpeed = 1f;
    [SerializeField] float RunningSpeed = 2.5f;

    [Header("Debug")]
    [SerializeField] bool deathtTest;

    public bool HasTarget => AI.Target != null;
    public bool IsAlive => Health.Amount > 0;

    public static event Action Death;
    [field: SerializeField] public bool IsStunned { get; private set; }

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
        NavMeshAgent.isStopped = true;
        NavMeshAgent.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.useGravity = false;
        Collider.enabled = false;

        if (useRagdoll)
        {
            Animator.StopPlayback();
            Animator.enabled = false;
            
            Ragdoll.TurnOn();
        }
        else Animator.SetTrigger("Death");

        Death?.Invoke();
    }

    private void OnDamaged(GameObject damageGiver)
    {
        if (damageGiver.TryGetComponent<Player>(out Player player)) AI.SetTarget(player);
        NavMeshAgent.isStopped = true;
        Animator.SetTrigger("Damaged");
        IsStunned = true;
    }

    public void DamageFinish()
    {
        if (NavMeshAgent.isActiveAndEnabled) NavMeshAgent.isStopped = false;
        IsStunned = false;
    }
}
