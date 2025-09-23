using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLoader : IAsyncStartable
{
    [Inject] LevelLoader levelLoader;
    [Inject] AvatarCameraService avatarCameraService;
    [Inject] AvatarControllerService avatarControllerService;
    [Inject] MoveTargetMakerService moveTargetMakerService;
    [Inject] AvatarContainer avatarContainer;
    [Inject] AiChatService aiChatService;
    
    bool isNetworkStarted = false;
    
    public async UniTask StartAsync(CancellationToken cancellation = new())
    {
        AnimationEnum.Initialize();
        
        aiChatService.Initialize();
        
        var isLoaded = await levelLoader.LoadAsync(1);
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
        
        await moveTargetMakerService.InitializeAsync();
        
        avatarCameraService.Initialize(mainAvatar);
        avatarControllerService.Possession(mainAvatar);
    }
}
