using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class CameraLook : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Camera _playerCamera;

        [Header("Settings")] 
        [SerializeField] private float _mouseSensitivity = 100f;
        
        private float _xRotation = 0f;
        private Vector2 _mouseInput;

        private void Start()
        {
            HideCursor(true);
        }

        private void Update()
        {
            ProccessLook();
        }

        private void OnLook(InputValue value)
        {
            _mouseInput = value.Get<Vector2>();
        }

        private void HideCursor(bool hide)
        {
            if (hide == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            Cursor.visible = !hide;
        }

        private void ProccessLook()
        {
            float mouseX = _mouseInput.x * _mouseSensitivity * Time.deltaTime;
            float mouseY = _mouseInput.y * _mouseSensitivity * Time.deltaTime;
            
            transform.Rotate(Vector3.up * mouseX);
            
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
}
}

