using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CharacterController characterController;

        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpHeight = 1.5f;

        private Vector2 _movementInput;
        private Vector3 _playerVelocity;

        private void Update()
        {
            ProccessMovement();
        }

        private void OnMove(InputValue value)
        {
            _movementInput = value.Get<Vector2>();
        }

        private void OnJump(InputValue value)
        {
            if (value.isPressed && characterController.isGrounded)
            {
                _playerVelocity.y = Mathf.Sqrt(2f * -Physics.gravity.y * _jumpHeight);
            }
        }

        private void ProccessMovement()
        {
            Vector3 movementDirection = transform.forward * _movementInput.y +
                                        transform.right * _movementInput.x;

            movementDirection.Normalize();
            characterController.Move(movementDirection * (_moveSpeed * Time.deltaTime));
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(_playerVelocity * Time.deltaTime);
        }
    }
}