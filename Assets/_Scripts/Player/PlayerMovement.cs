using System;
using UnityEngine;
using UnityEngine.InputSystem;
using JustAnotherMinecraft.GeneralSystems;


namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterController characterController;

        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpHeight = 1f;

        private Vector2 _movementInput;
        private Vector3 _playerVelocity;
        private bool _isDisabled;

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void Update()
        {
            ProccessMovement();
            ApplyGravity();
        }

        private void OnMove(InputValue value)
        {
            _movementInput = value.Get<Vector2>();
        }

        private void OnJump(InputValue value)
        {
            if(_isDisabled) return;
            if (value.isPressed && characterController.isGrounded)
            {
                _playerVelocity.y = Mathf.Sqrt( -Physics.gravity.y * _jumpHeight);
            }
        }

        private void ProccessMovement()
        {
            Vector3 movementDirection = Vector3.zero;
            if (!_isDisabled)
            {
                movementDirection = transform.forward * _movementInput.y +
                                            transform.right * _movementInput.x;
            }
            
            movementDirection.Normalize();
            characterController.Move(movementDirection * (_moveSpeed * Time.deltaTime));
            //ApplyGravity();
        }

        private void ApplyGravity()
        {
            _playerVelocity.y += (Physics.gravity.y * Time.deltaTime) / 2f;
            characterController.Move(_playerVelocity * Time.deltaTime);
        }
        
        private void SubscribeToEvents()
        {
            GameEvents.onInventoryToggle += DisablePlayerMovement;
        }

        private void DisablePlayerMovement(bool _input)
        {
            _isDisabled = _input;
        }
    }
}