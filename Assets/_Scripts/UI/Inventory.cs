using System;
using UnityEngine;
using JustAnotherMinecraft.GeneralSystems;

namespace JustAnotherMinecraft.UI
{
    public class Inventory : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _inventoryUI;
        private void OnEnable()
        {
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            GameEvents.onInventoryToggle += ToggleInventory;
        }
        
        private void ToggleInventory(bool isOpen)
        {
            _inventoryUI.SetActive(isOpen);
        }
    }
}

