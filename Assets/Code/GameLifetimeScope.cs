using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using Unity.Netcode;
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
        
        /*builder.RegisterFactory<ulong, bool>(container =>
        {
            bool InstantiateAvatar(ulong clientId)
            {
                return SpawnPlayer(clientId);
                /*var newGameObject = Instantiate(avatarPrefab, Vector3.zero, Quaternion.identity);
                newGameObject.name = $"Avatar_{avatarSequence}";

                //var newAvatar = newGameObject.AddComponent<Avatar>();
                //newGameObject.AddComponent<NavMeshMover>();
                //newGameObject.AddComponent<AvatarAnimator>();

                //Add Behaviour
                //newGameObject.AddComponent<GoapActionProvider>();
                //newGameObject.AddComponent<AgentBehaviour>();

                return newGameObject.GetComponent<Avatar>();#1#
            }
            
            return InstantiateAvatar;
        }, Lifetime.Singleton);*/

        builder.Register<AvatarModelService>(Lifetime.Singleton);
        builder.Register<AvatarLoader>(Lifetime.Singleton);
        builder.Register<AvatarContainer>(Lifetime.Singleton);
        builder.Register<LevelLoader>(Lifetime.Singleton);
        builder.Register<AvatarControllerService>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameLoader>(Lifetime.Singleton);
        builder.RegisterEntryPoint<MainAvatarHPTickService>(Lifetime.Singleton);
        builder.Register<MainAvatarData>(Lifetime.Singleton);
        builder.Register<MoveTargetMakerService>(Lifetime.Singleton);
        
        DontDestroyOnLoad(gameObject);
    }

    /*private bool SpawnPlayer(ulong clientId)
    {
        var newGameObject = NetworkManager.Instantiate(avatarPrefab, Vector3.zero, Quaternion.identity);
        //newGameObject.name = $"Avatar_{avatarSequence}";
        
        var networkObject = newGameObject.GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.Log($"networkObject is null. {clientId}");
            return false;
        }
        
        //Container.InjectGameObject(newGameObject);
        
        
        
        /*if (networkObject.IsOwner)
        {
            newGameObject.AddComponent<GoapActionProvider>();
            newGameObject.AddComponent<AgentBehaviour>();
        }#1#
        
        //var avatarContainer = Container.Resolve<AvatarContainer>();
        //var avatar = newGameObject.GetComponent<Avatar>();
        //avatar.Initialize(avatarSequence);
        //avatarContainer.Add(clientId, avatar);
        
        networkObject.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
        
        return true;
    }*/
    
    
}
