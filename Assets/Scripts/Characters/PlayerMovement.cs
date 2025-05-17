using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    [SerializeField] float speed;
    [SerializeField] float runningSpeed;
    [SerializeField] Camera cam;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.Input.Movement.magnitude > 0)
        {
            // Rotate to match camera's horizontal forward
            Vector3 lookDirection = cam.transform.forward;
            lookDirection.y = 0f;
            lookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            Vector3 position = transform.position;
            position += transform.forward * player.Input.Movement.y * Time.deltaTime * speed;
            position += transform.right * player.Input.Movement.x * Time.deltaTime * speed;
            transform.position = position;
        }

        player.Animator.SetFloat("Forward", player.Input.Movement.y, 0.1f, Time.deltaTime);
        player.Animator.SetFloat("Right", player.Input.Movement.x, 0.1f, Time.deltaTime);
    }
}
