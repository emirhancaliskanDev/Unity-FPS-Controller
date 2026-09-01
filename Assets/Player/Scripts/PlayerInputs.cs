using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    public static PlayerInputs Instance;

    [SerializeField]Vector3 movementDirection;

    //References
    [SerializeField]Camera fpsCamera;

    //Inputs
    Vector2 movementInputVector;
    

    public event Action OnJumpEvent;
    public event Action<bool> OnSprintEvent;
    MainInputActions inputActions;


    bool isSprinting;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        isSprinting = false;

        inputActions = new MainInputActions();
        inputActions.Enable();


        inputActions.Player.Movement.performed += MovementInput;
        inputActions.Player.Movement.canceled += MovementInput;
        inputActions.Player.Jump.started += JumpInput;
        inputActions.Player.Sprint.performed += SprintInput;
        inputActions.Player.Sprint.canceled += SprintInput;
        
    }

    private void SprintInput(InputAction.CallbackContext context)
    {
        if (isSprinting != context.ReadValueAsButton())
        {
            isSprinting = context.ReadValueAsButton();
            OnSprintEvent?.Invoke(isSprinting);
        }
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        OnJumpEvent?.Invoke();
    }

    void Update()
    {
        SetMovementDirection();
    }

    private void MovementInput(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
    }

    void SetMovementDirection()
    {
        movementDirection = fpsCamera.transform.forward * movementInputVector.y + fpsCamera.transform.right * movementInputVector.x;
        movementDirection.y = 0;
    }

    public Vector3 GetMovementDirection => movementDirection;
}
