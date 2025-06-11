using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpHeight = 1.5f;

        [Header("References")] 
        [SerializeField] CharacterController characterController;
        
        Vector2 movementInput;
        Vector3 playerVelocity;

        private void Update()
        {
            ProccessMovement();
        }

        void OnMove(InputValue value)
        {
            movementInput = value.Get<Vector2>();
        }
        
        void OnJump(InputValue value)
        {
            if (value.isPressed && characterController.isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(2f * -Physics.gravity.y * jumpHeight);
            }
        }

        void ProccessMovement()
        {
            Vector3 movementDirection = transform.forward * movementInput.y + 
                                        transform.right * movementInput.x;
            
            movementDirection.Normalize();
            characterController.Move(movementDirection * (moveSpeed * Time.deltaTime));
            ApplyGravity();
        }

        void ApplyGravity()
        {
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }
}