using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;


namespace JustAnotherMinecraft.UI
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("References")]
        [SerializeField] private Image _image;
        public TextMeshProUGUI CountText;
        private GridLayoutGroup _parentGrid;
        
        
        [HideInInspector] public Item Item;
        [HideInInspector] public int Count = 1;
        [HideInInspector] public Transform ParentAfterDrag;

        private void OnEnable()
        {
            GrabDependency();
        }

        public void InitializeItem(Item newItem)
        {
            Item = newItem;
            _image.sprite = newItem.Image;
            RefreshCount();
        }

        public void RefreshCount()
        {
            CountText.text = Count.ToString();
            bool textVisible = Count > 1;
            CountText.gameObject.SetActive(textVisible);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parentGrid = transform.parent.parent.GetComponent<GridLayoutGroup>();
            _image.raycastTarget = false;
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.parent.parent.parent);
            _parentGrid.enabled = false;
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _parentGrid.enabled = true;
            _image.raycastTarget = true;
            transform.SetParent(ParentAfterDrag);
        }

        private void GrabDependency()
        {
            _image = GetComponent<Image>();
        }
    }
}

