using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    public Vector2 GetMovementInput() => inputActions.Movement.Move.ReadValue<Vector2>();
    public bool IsRunning() => inputActions.Movement.Run.IsPressed();
    public bool JumpTriggered() => inputActions.Movement.Jump.triggered;
    public Vector2 GetLookInput() => inputActions.Movement.Look.ReadValue<Vector2>();
    public bool IsAttackPressed() => inputActions.Combat.Attack.WasPressedThisFrame();

    private void OnDestroy()
    {
        inputActions.Disable();
    }
}
