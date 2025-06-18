using UnityEngine;
using JustAnotherMinecraft.UI;
using UnityEditor.UIElements;


namespace JustAnotherMinecraft.GeneralSystems
{
    public class InventoryManager : MonoBehaviour
    {
        public ToolBarSlot[] ToolBarSlots;
        public InventorySlot[] InventorySlots;
        public GameObject InventoryItemPrefab;

        int _selectedSlotIndex = -1;

        private void Awake()
        {
            SubscribeToEvents();
        }
        
        private void Start()
        {
            ChangeSelectedSlot(0);
        }
        
        private void ChangeSelectedSlot(int newIndex)
        {
            if(_selectedSlotIndex >= 0) ToolBarSlots[_selectedSlotIndex].Deselect();
            
            ToolBarSlots[newIndex].Select();
            _selectedSlotIndex = newIndex;
        }

        public bool AddItem(Item item)
        {
            for(int i = 0; i < InventorySlots.Length; i++)
            {
                if (InventorySlots[i].transform.childCount == 0)
                {
                    InventorySlot slot = InventorySlots[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                    if (itemInSlot != null &&
                        itemInSlot.Item == item &&
                        itemInSlot.Count < item.MaxStackSize &&
                        itemInSlot.Item.Stackable)
                    {
                        itemInSlot.Count++;
                        itemInSlot.RefreshCount();
                        return true;
                    }
                }
            }
            
            for(int i = 0; i < InventorySlots.Length; i++)
            {
                if (InventorySlots[i].transform.childCount == 0)
                {
                    InventorySlot slot = InventorySlots[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                    if (itemInSlot == null)
                    {
                        SpawnNewItem(item, slot);
                        return true;
                    }
                }
            }

            return false;
        }

        private void SpawnNewItem(Item item, InventorySlot slot)
        {
            GameObject newItemGO = Instantiate(InventoryItemPrefab, slot.transform);
            InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
            inventoryItem.InitializeItem(item);
        }
        
        private void SubscribeToEvents()
        {
            GameEvents.onSwitchToolbarSlot += ChangeSelectedSlot;
        }
    }    
}

