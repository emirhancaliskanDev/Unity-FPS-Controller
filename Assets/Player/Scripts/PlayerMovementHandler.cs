using System;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField]float movementSpeed;
    [SerializeField]float walkSpeed;
    [SerializeField]float sprintSpeed;
    [SerializeField]float jumpForce;
    [SerializeField]float jumpCooldown;
    [SerializeField]bool canJump;
    [SerializeField]bool canSprint;
    Rigidbody playerRigidbody;
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        
    }
    void Start()
    {
        PlayerInputs.Instance.OnJumpEvent += Jump;
        PlayerInputs.Instance.OnSprintEvent += Sprint;
        StaminaHandler.Instance.OnStaminaZero += resetCanDo;
        StaminaHandler.Instance.CanJump += ResetCanJump;
        StaminaHandler.Instance.CanSprint += ResetCanSprint;
        canJump = true;
        canSprint = true;
        movementSpeed = walkSpeed;
    }

    private void resetCanDo()
    {
        canSprint = false;
        canJump = false;
        Sprint(false);
    }

    private void Sprint(bool obj)
    {
        switch (obj)
        {
            case true:
                if (StaminaHandler.Instance.GetStamina > StaminaHandler.Instance.GetReduceAmount && canSprint)
                {
                    SetMovementSpeed(sprintSpeed);
                    StaminaHandler.Instance.SetUsingMode(true);
                    StaminaHandler.Instance.SetRefreshMode(false);    
                }
                
                break;

            case false:
                SetMovementSpeed(walkSpeed);
                StaminaHandler.Instance.SetUsingMode(false);
                StaminaHandler.Instance.SetRefreshMode(true);
                break;
        }
    }

    private void Jump()
    {
        if (canJump && GeneralHelpers.Instance.GroundCheck())
        {
            if (StaminaHandler.Instance.GetStamina > 10f)
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Force);
                canJump = false;
                Invoke(nameof(ResetCanJump),jumpCooldown);
                StaminaHandler.Instance.ReduceStamina(10f);
                if (movementSpeed == 45)
                {
                    StaminaHandler.Instance.SetUsingMode(true);
                    StaminaHandler.Instance.SetRefreshMode(false);
                }else
                {
                    StaminaHandler.Instance.SetUsingMode(false);
                    StaminaHandler.Instance.SetRefreshMode(true);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        playerRigidbody.AddRelativeForce(PlayerInputs.Instance.GetMovementDirection * movementSpeed,ForceMode.Force);
    }

    void ResetCanJump()
    {
        canJump = true;
    }
    void ResetCanSprint()
    {
        canSprint = true;
    }

    void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

}
