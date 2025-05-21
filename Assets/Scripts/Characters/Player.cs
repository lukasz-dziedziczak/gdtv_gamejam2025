using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public InputReader Input { get; private set; }
    [field: SerializeField] public PlayerShooting Shooting { get; private set; }
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public CoinHolder CoinHolder { get; private set; }
    [field: SerializeField] public A_Footstep Footstep { get; private set; }
    [field: SerializeField] public A_RifleMuzzle RifleMuzzle { get; private set; }
    [field: SerializeField] public A_Voice Voice { get; private set; }
    [field: SerializeField] public A_PlayerImpact Impact { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        UI.HealthBar.Initilize();
    }

    private void OnEnable()
    {
        Health.OnDeath += OnDeath;
        Health.OnDamaged += OnDamaged;
        Input.Pause += OnPausePress;
    }

    private void OnDisable()
    {
        Health.OnDeath -= OnDeath;
        Health.OnDamaged -= OnDamaged;
        Input.Pause -= OnPausePress;
    }

    public void PlayFootstep()
    {
        Footstep.PlayFootstepSound();
    }

    public void PlayRemoveClipSound()
    {
        RifleMuzzle.PlayRemoveClipSound();
    }

    public void PlayLoadClipSound()
    {
        RifleMuzzle.PlayLoadClipSound();
    }

    public void PlayReloadCompleteSound()
    {
        RifleMuzzle.PlayReloadCompleteSound();
    }

    public bool IsAlive => Health.Amount > 0;

    private void OnDeath()
    {
        Animator.SetTrigger("Die");
    }

    private void OnDamaged(GameObject damageGiver)
    {
        Impact.PlayImpactSound();
    }

    private void OnPausePress()
    {
        UI.TogglePaused();
    }
}
