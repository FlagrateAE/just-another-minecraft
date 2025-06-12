using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private World _world;

    // private void OnHit(InputValue value)
    // {
    //     if (value.isPressed)
    //     {
    //         Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

    //         if (Physics.Raycast(ray, out RaycastHit hitInfo))
    //         {

    //         }
    //     }
    // }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3Int blockGlobalPosition = Vector3Int.FloorToInt(hitInfo.point + hitInfo.normal / 2);

                _world.SetBlock(blockGlobalPosition, BlockType.Dirt);
            }
        }
    }
}