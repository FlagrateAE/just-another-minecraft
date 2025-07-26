using System.Collections.Generic;
using JustAnotherMinecraft.GeneralSystems;
using UnityEngine;


namespace JustAnotherMinecraft.UI
{
    public class ToolBarCopy : MonoBehaviour
    {
        [SerializeField] private GameObject _originalToolBar;
        [SerializeField] private InventoryManager _inventoryManager;
        
        private void OnEnable()
        {
            SynchronizeToolBar(_originalToolBar, gameObject);
        }
        
        private void SynchronizeToolBar(GameObject fromWhich, GameObject toWhich)
        {
            List<GameObject> mainToolBarSlots = GetChildren(fromWhich);
            List<GameObject> newToolBarSlots = GetChildren(toWhich);
            
            for(int i = 0; i < mainToolBarSlots.Count; i++)
            {
                if (i < newToolBarSlots.Count)
                {
                    GameObject originalSlot = mainToolBarSlots[i];
                    GameObject newSlot = newToolBarSlots[i];
                    
                    InventorySlot originalToolBarSlot = originalSlot.GetComponent<InventorySlot>();
                    
                    GameObject originalSlotItem = originalToolBarSlot.GetComponentInChildren<InventoryItem>()?.gameObject;
                    
                    if(newSlot.transform.childCount > 0)
                    {
                        Destroy(newSlot.transform.GetChild(0).gameObject);
                    }
                    
                    if (originalSlotItem != null)
                    {
                        Instantiate(originalSlotItem, newSlot.transform);
                    }
                }
            }
        }
        
        private void OnDisable()
        {
            SynchronizeToolBar(gameObject, _originalToolBar);
        }
        
        private List<GameObject> GetChildren(GameObject givenGameObject)
        {
            List<GameObject> childrenList = new List<GameObject>();
            
            foreach (Transform childTransform in givenGameObject.transform)
            {
                childrenList.Add(childTransform.gameObject);
            }
            
            return childrenList;
        }
    }
}

