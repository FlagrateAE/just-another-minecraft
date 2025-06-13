using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Entity))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Entity _entity;

    [Inject] private World _world;

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Vector3 rayOrigin = _entity.Head.position;
            Vector3 rayDirection = _entity.Head.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, _entity.Reach))
            {
                Vector3Int blockGlobalPosition = Vector3Int.FloorToInt(hitInfo.point + hitInfo.normal / 2);

                _world.PlaceBlock(blockGlobalPosition, BlockId.OakPlanks);
            }
        }
    }
}