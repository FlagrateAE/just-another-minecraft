using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using JustAnotherMinecraft.GeneralSystems;

namespace JustAnotherMinecraft.Player
{
    [RequireComponent(typeof(LivingEntity))]
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LivingEntity _entity;

        [Inject] private World _world;
        private bool _isEnabled = true;
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnInteract(InputValue value)
        {
            if (value.isPressed)
            {
                if(!_isEnabled) return;
                
                Debug.Log("Interact");
                Vector3 rayOrigin = _entity.Head.position;
                Vector3 rayDirection = _entity.Head.forward;

                if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, _entity.Reach))
                {
                    if (hitInfo.transform.TryGetComponent(out Chunk _))
                    {
                        Vector3Int position = Vector3Int
                            .FloorToInt(hitInfo.point + hitInfo.normal / 2);

                        _world.TryPlaceBlock(position, BlockId.OakPlanks);
                    }
                }
            }
        }
        
        private void SubscribeToEvents()
        {
            GameEvents.onInventoryToggle += TogglePlayerInteraction;
        }
    
        private void TogglePlayerInteraction(bool isOpen)
        {
            _isEnabled = !isOpen;
        }
    }
}
