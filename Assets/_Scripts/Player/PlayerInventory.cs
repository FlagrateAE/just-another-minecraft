using UnityEngine;
using UnityEngine.InputSystem;
using JustAnotherMinecraft.GeneralSystems;

namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] InputActionReference _inventoryAction;
        private bool _isInventoryOpen = false;

        private void OnInventory(InputValue value)
        {
            if (value.isPressed)
            {
                _isInventoryOpen = !_isInventoryOpen;
                GameEvents.InventoryToggle(_isInventoryOpen);
            }
        }

        private void OnSwitch(InputValue value)
        {
            int result = -1;
            for (int i = 0; i < _inventoryAction.action.bindings.Count; i++)
            {
                if (_inventoryAction.action.controls[i].IsPressed())
                {
                    result = i;
                }
            }
            
            if(result >= 0 && result <= 8)
            {
                GameEvents.SwitchToolbarSlot(result);
            }
            else
            {
                Debug.LogWarning("Invalid toolbar slot index pressed: " + (result));
            }
        }
    }
}

