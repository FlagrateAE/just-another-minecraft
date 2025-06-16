using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


namespace JustAnotherMinecraft.UI
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("References")]
        [SerializeField] private Image _image;
        private GridLayoutGroup _parentGrid;

        [HideInInspector] public Transform parentAfterDrag;

        private void OnEnable()
        {
            GrabDependency();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parentGrid = transform.parent.parent.GetComponent<GridLayoutGroup>();
            _image.raycastTarget = false;
            parentAfterDrag = transform.parent;
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
            transform.SetParent(parentAfterDrag);
        }

        private void GrabDependency()
        {
            _image = GetComponent<Image>();
        }
    }
}

