using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerLocomotion _playerLocomotion;
    private AnimatorManager _animatorManager;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _animatorManager = GetComponent<AnimatorManager>();
    }

    private void Update()
    {
        _inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
    }
}
