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

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        UI.HealthBar.Initilize();
    }
}
