using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(LivingEntity))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LivingEntity _entity;

    // [Inject] private World _world;

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Vector3 rayOrigin = _entity.Head.position;
            Vector3 rayDirection = _entity.Head.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, _entity.Reach))
            {
                if (hitInfo.transform.TryGetComponent(out Chunk chunk))
                {
                    Vector3Int localPosition = Vector3Int
                    .FloorToInt(hitInfo.point + hitInfo.normal / 2)
                    .GlobalToLocalPosition();

                    chunk.TryPlaceBlock(localPosition, BlockId.OakPlanks);
                }
            }
        }
    }
}