using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class CameraLook : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] GameObject playerBody;
        [SerializeField] Camera playerCamera;

        [Header("Settings")] 
        [SerializeField] float mouseSensitivity = 100f;
        
        float xRotation = 0f;
        Vector2 mouseInput;
        void Start()
        {
            HideCursor(true);
        }

        private void Update()
        {
            ProccessLook();
        }

        void OnLook(InputValue value)
        {
            mouseInput = value.Get<Vector2>();
        }

        void HideCursor(bool hide)
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

        void ProccessLook()
        {
            float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;
            
            playerBody.transform.Rotate(Vector3.up * mouseX);
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
}
}

