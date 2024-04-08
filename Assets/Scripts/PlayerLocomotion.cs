using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private PlayerManager _playerManager;
    private InputManager _inputManager;
    private AnimatorManager _animatorManager;
    
    private Vector3 _moveDirection;
    private Transform _cameraObject;
    private Rigidbody _rb;

    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    public bool isSwordHitting;

    private void Awake()
    {
        _playerManager = GetComponent<PlayerManager>();
        _inputManager = GetComponent<InputManager>();
        _animatorManager = GetComponent<AnimatorManager>();
        _rb = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        //HandleSwordHitting();
        
        if (_playerManager.isInteracting) return;
        
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        _moveDirection = _cameraObject.forward * _inputManager.verticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * _inputManager.horizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        _moveDirection = _moveDirection * movementSpeed;

        Vector3 movementVelocity = _moveDirection;
        _rb.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = _cameraObject.forward * _inputManager.verticalInput;
        targetDirection = targetDirection + _cameraObject.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleSwordHitting()
    {
        if (!_playerManager.isInteracting)
        {
            _animatorManager.PlayTargetAnimation("SwordHit", true);
        }
    }
}
