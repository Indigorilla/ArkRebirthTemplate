using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameLoader : IAsyncStartable
{
    [Inject] LevelLoader levelLoader;
    [Inject] AvatarLoader avatarLoader;
    [Inject] AvatarCameraService avatarCameraService;
    [Inject] AvatarControllerService avatarControllerService;
    [Inject] MoveTargetMakerService moveTargetMakerService;
    [Inject] AvatarContainer avatarContainer;
    
    bool isNetworkStarted = false;
    
    public async UniTask StartAsync(CancellationToken cancellation = new())
    {
        AnimationEnum.Initialize();
        
        NetworkManager.Singleton.OnConnectionEvent += (manager, data) =>
        {
            Debug.Log($"Client Started. {NetworkManager.Singleton.LocalClientId}");
            isNetworkStarted = true;
        };
        
        while (!isNetworkStarted)
        {
            await UniTask.Yield();
        }
        
        var isLoaded = await levelLoader.Load(1);
        if (!isLoaded)
        {
            Debug.Log("Level Load Failed.");
            return;
        }

        var mainAvatar = avatarContainer.Get(NetworkManager.Singleton.LocalClientId);
        while (mainAvatar == null)
        {
            mainAvatar = avatarContainer.Get(NetworkManager.Singleton.LocalClientId);
            await UniTask.Yield();
        }
        
        /*var mainAvatar = avatarLoader.Load(NetworkManager.Singleton.LocalClientId);
        if (mainAvatar == null)
        {
            Debug.Log("Avatar Load Failed.");
            return;
        }*/
        
        await moveTargetMakerService.Initialize();
        
        avatarCameraService.Initialize(mainAvatar);
        avatarControllerService.Possession(mainAvatar);
    }
}
