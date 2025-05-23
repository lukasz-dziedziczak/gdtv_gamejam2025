using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    [SerializeField] float walkingSpeed;
    [SerializeField] float runningSpeed;
    [SerializeField] Camera cam;
    [SerializeField] float jump;
    [field: SerializeField] public bool IsJumping { get; private set; }
    [field: SerializeField] public bool HasMoved { get; private set; }

    private float speed
    {
        get
        {
            if (player.Shooting.IsReloading) return walkingSpeed / 3;
            else if (IsSprinting) return runningSpeed;
            else return walkingSpeed;
        }
    }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.Input.Jump += OnJump;
    }

    private void OnDisable()
    {
        player.Input.Jump -= OnJump;
    }

    private void Update()
    {
        if (!player.IsAlive || Time.timeScale == 0) return; 

        // Rotate to match camera's horizontal forward
        Vector3 lookDirection = cam.transform.forward;
        lookDirection.y = 0f;
        lookDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        if (player.Input.Movement.magnitude > 0)
        {
            Vector3 position = transform.position;
            position += transform.forward * player.Input.Movement.y * Time.deltaTime * speed;
            position += transform.right * player.Input.Movement.x * Time.deltaTime * speed;
            transform.position = position;

            HasMoved = true;
        }

        player.Animator.SetFloat("Forward", (IsSprinting ? player.Input.Movement.y * 2 : player.Input.Movement.y), 0.1f, Time.deltaTime);
        player.Animator.SetFloat("Right", player.Input.Movement.x, 0.1f, Time.deltaTime);
    }

    private void OnJump()
    {
        if (!player.IsAlive || 
            Time.timeScale == 0 || 
            player.Shooting.IsReloading || 
            player.Movement.IsJumping) 
                return;

        player.Rigidbody.AddForce(transform.up * jump, ForceMode.Acceleration);
        player.Animator.SetTrigger("Jump");
        IsJumping = true;
        player.Footstep.PlayFootstepSound();
    }

    public void JumpComplete()
    {
        IsJumping = false;
    }

    public bool IsSprinting =>
        !player.Movement.IsJumping &&
        !player.Shooting.IsReloading &&
        !player.Input.IsAttacking && 
        player.Input.IsSprinting && 
        player.Input.Movement.x <= 0.25f &&
        player.Input.Movement.x >= -0.25f &&
        player.Input.Movement.y > 0;
}
