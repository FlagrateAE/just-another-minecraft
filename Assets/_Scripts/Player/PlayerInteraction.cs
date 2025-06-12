using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private World _world;

    [Header("Settings")]
    [SerializeField] private float _reach = 7f;

    private void OnHit(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _reach))
            {
                Vector3Int blockGlobalPosition = Vector3Int.FloorToInt(hitInfo.point - hitInfo.normal / 2);
                _world.BreakBlock(blockGlobalPosition);
            }
        }
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _reach))
            {
                Vector3Int blockGlobalPosition = Vector3Int.FloorToInt(hitInfo.point + hitInfo.normal / 2);

                _world.PlaceBlock(blockGlobalPosition, BlockId.Dirt);
            }
        }
    }
}