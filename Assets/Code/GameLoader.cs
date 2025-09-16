using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLoader : IAsyncStartable
{
    [Inject] LevelLoader levelLoader;
    [Inject] AvatarLoader avatarLoader;
    [Inject] AvatarCameraService avatarCameraService;
    [Inject] AvatarControllerService avatarControllerService;
    [Inject] MoveTargetMakerService moveTargetMakerService;
    
    public async UniTask StartAsync(CancellationToken cancellation = new())
    {
        AnimationEnum.Initialize();
        
        var isLoaded = await levelLoader.Load(1);
        if (!isLoaded)
        {
            Debug.Log("Level Load Failed.");
            return;
        }
        
        var mainAvatar = avatarLoader.Load(1);
        if (mainAvatar == null)
        {
            Debug.Log("Avatar Load Failed.");
            return;
        }
        
        await moveTargetMakerService.Initialize();
        
        avatarCameraService.Initialize(mainAvatar);
        avatarControllerService.Possession(mainAvatar);
    }
}
