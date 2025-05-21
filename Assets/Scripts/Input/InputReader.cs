using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    InputSystem_Actions actions;

    [field: SerializeField] public Vector2 Movement { get; private set; }
    [field: SerializeField] public Vector2 Look { get; private set; }
    [field: SerializeField] public bool IsAttacking { get; private set; }
    [field: SerializeField] public bool IsSprinting { get; private set; }
    public event Action Attack;
    public event Action AttackComplete;
    public event Action Jump;
    public event Action Reload;
    public event Action Pause;

    private void Start()
    {
        actions = new InputSystem_Actions();
        actions.Player.AddCallbacks(this);
        actions.Player.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
            Attack?.Invoke();
        }
        else if (context.canceled)
        {
            IsAttacking = false;
            AttackComplete?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) Jump?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) IsSprinting = true;
        else if (context.canceled) IsSprinting = false;
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed) Reload?.Invoke();
        
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) Pause?.Invoke();
    }
}
