using CrashKonijn.Goap.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private AvatarSettings avatarSettings;
    [SerializeField] private AvatarAnimationSettings avatarAnimationSettings;
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private AvatarCameraService avatarCameraService;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GoapBehaviour goap;
    [SerializeField] private Avatar avatarPrefab;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(avatarSettings);
        builder.RegisterInstance(avatarAnimationSettings);
        builder.RegisterInstance(levelSettings);
        builder.RegisterInstance(avatarCameraService);
        builder.RegisterInstance(mainCamera);
        builder.RegisterInstance(goap);
        builder.RegisterInstance(avatarPrefab);
        
        builder.Register<AvatarModelService>(Lifetime.Singleton);
        builder.Register<AvatarContainer>(Lifetime.Singleton);
        builder.Register<LevelLoader>(Lifetime.Singleton);
        builder.Register<AvatarControllerService>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameLoader>(Lifetime.Singleton);
        builder.RegisterEntryPoint<MainAvatarHPTickService>(Lifetime.Singleton);
        builder.Register<MainAvatarData>(Lifetime.Singleton);
        builder.Register<MoveTargetMakerService>(Lifetime.Singleton);
        
        DontDestroyOnLoad(gameObject);
    }
}
