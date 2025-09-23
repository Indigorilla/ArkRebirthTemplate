using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private AiChatBubble aiChatBubblePrototype;
    [SerializeField] private AvatarCameraService avatarCameraService;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(aiChatBubblePrototype);
        builder.RegisterInstance(avatarCameraService);
        
        builder.Register<AiChatService>(Lifetime.Singleton);
        builder.Register<AvatarControllerService>(Lifetime.Singleton);
        builder.Register<LevelLoader>(Lifetime.Singleton);
        builder.Register<MainAvatarData>(Lifetime.Singleton);
        builder.Register<MoveTargetMakerService>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<GameLoader>();
        builder.RegisterEntryPoint<MainAvatarHPTickService>();
        
        builder.RegisterFactory<Transform, AiChatBubble>(container =>
        {
            AiChatBubble InstantiateChatBox(Transform parent)
            {
                var newChatBox = container.Instantiate(aiChatBubblePrototype, aiChatBubblePrototype.transform.parent);
                newChatBox.Initialize(parent, new Vector3(0f, 3f, 0f));
                return newChatBox;
            }
            
            return InstantiateChatBox;
        }, Lifetime.Singleton);
    }
}
