using CrashKonijn.Goap.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private AvatarSettings avatarSettings;
    [SerializeField] private AvatarAnimationSettings avatarAnimationSettings;
    [SerializeField] private AiChatSetting aiChatSetting;
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private GoapBehaviour goap;
    [SerializeField] private Avatar avatarPrefab;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(avatarSettings);
        builder.RegisterInstance(avatarAnimationSettings);
        builder.RegisterInstance(aiChatSetting);
        builder.RegisterInstance(levelSettings);
        builder.RegisterInstance(goap);
        builder.RegisterInstance(avatarPrefab);
        
        builder.Register<AvatarModelService>(Lifetime.Singleton);
        builder.Register<AvatarContainer>(Lifetime.Singleton);
        
        DontDestroyOnLoad(gameObject);
    }
}
