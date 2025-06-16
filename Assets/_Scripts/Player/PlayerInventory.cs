using UnityEngine;
using UnityEngine.InputSystem;
using JustAnotherMinecraft.GeneralSystems;

namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInventory : MonoBehaviour
    {
        private bool _isInventoryOpen = false;
        
        private void OnInventory(InputValue value)
        {
            if (value.isPressed)
            {
                _isInventoryOpen = !_isInventoryOpen;
                GameEvents.InventoryToggle(_isInventoryOpen);
            }
        }
    }
}

