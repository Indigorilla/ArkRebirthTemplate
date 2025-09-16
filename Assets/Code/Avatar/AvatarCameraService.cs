using Unity.Cinemachine;
using UnityEngine;

public class AvatarCameraService : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;
    
    public void Initialize(Avatar targetAvatar)
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        cinemachineCamera.Target.TrackingTarget = targetAvatar.transform;
    }
}
