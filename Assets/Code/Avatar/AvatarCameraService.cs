using Unity.Cinemachine;
using UnityEngine;

public class AvatarCameraService : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera uiCamera;
    
    private CinemachineCamera cinemachineCamera;
    
    public void Initialize(Avatar targetAvatar)
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        cinemachineCamera.Target.TrackingTarget = targetAvatar.transform;
    }

    public bool MainCameraRaycast(Vector2 screenPosition, out Vector3 hitPoint)
    {
        var ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out var hit))
        {
            hitPoint = hit.point;
            return true;
        }

        hitPoint = Vector3.zero;
        return false;
    }
    
    public Vector3 WorldPotionToUIPosition(Vector3 worldPosition)
    {
        var screenPoint = mainCamera.WorldToScreenPoint(worldPosition);
        return uiCamera.ScreenToWorldPoint(screenPoint);
    }
}
