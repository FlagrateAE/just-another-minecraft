using System;
using UnityEngine;

namespace JustAnotherMinecraft.GeneralSystems
{
    public class GameEvents : MonoBehaviour
    {
        public static event Action<bool> onInventoryToggle;
        
        public static void InventoryToggle(bool isOpen)
        {
            onInventoryToggle?.Invoke(isOpen);
        }
    }
}