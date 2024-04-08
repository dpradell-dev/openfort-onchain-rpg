using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls;
    private PlayerLocomotion _playerLocomotion;
    private AnimatorManager _animatorManager;
    
    public Vector2 _movementInput;
    private float _moveAmount;

    public float verticalInput;
    public float horizontalInput;

    [HideInInspector] public bool swordHitInput;

    private void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            
            _playerControls.PlayerMovement.Movement.performed += Movement_OnPerformed_Handler;
            _playerControls.PlayerAttacks.SwordHit.performed += SwordHit_OnPerformed_Handler;
        }
        
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        //HandleSwordHitInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = _movementInput.y;
        horizontalInput = _movementInput.x;

        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, _moveAmount);
    }

    private void HandleSwordHitInput()
    {
        if (swordHitInput)
        {
            swordHitInput = false;
            //TODO
        }
    }

    private void Movement_OnPerformed_Handler(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }
    
    private void SwordHit_OnPerformed_Handler(InputAction.CallbackContext context)
    {
        _playerLocomotion.HandleSwordHitting();
    }
}
