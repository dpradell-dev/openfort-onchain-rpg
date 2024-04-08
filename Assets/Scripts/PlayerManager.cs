using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerLocomotion _playerLocomotion;
    private AnimatorManager _animatorManager;

    [HideInInspector] public bool isInteracting;

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
        _playerLocomotion.isSwordHitting = _animatorManager.animator.GetBool(_animatorManager.isSwordHitting);
    }

    private void LateUpdate()
    {
        isInteracting = _animatorManager.animator.GetBool(_animatorManager.isInteracting);
    }
}
