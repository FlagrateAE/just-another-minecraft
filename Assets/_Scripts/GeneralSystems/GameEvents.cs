using System;
using UnityEngine;

namespace JustAnotherMinecraft.GeneralSystems
{
    public class GameEvents : MonoBehaviour
    {
        public static event Action<bool> onInventoryToggle;
        public static event Action<int> onSwitchToolbarSlot;
        
        public static void InventoryToggle(bool isOpen)
        {
            onInventoryToggle?.Invoke(isOpen);
        }
        
        public static void SwitchToolbarSlot(int slotIndex)
        {
            onSwitchToolbarSlot?.Invoke(slotIndex);
        }
    }
}