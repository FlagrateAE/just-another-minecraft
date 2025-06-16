using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(LivingEntity))]
public class PlayerHitting : MonoBehaviour
{
    
    [SerializeField] private LivingEntity _entity;

    [Inject] private World _world;

    private void OnHit(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Hit");
            Vector3 rayOrigin = _entity.Head.position;
            Vector3 rayDirection = _entity.Head.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, _entity.Reach))
            {
                if (hitInfo.transform.TryGetComponent(out Chunk _))
                {
                    Vector3Int position = Vector3Int
                    .FloorToInt(hitInfo.point - hitInfo.normal / 2);

                    _world.BreakBlock(position);
                }
            }
        }
    }
}