using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

public class AvatarLoader
{
    //[Inject] private GameLifetimeScope lifetimeScope;
    //[Inject] private Func<ulong, bool> avatarFactory;
    [Inject] private AvatarContainer avatarContainer;
    [Inject] private Avatar avatarPrefab;
    
    public bool Load(ulong clientId)
    {
        if (avatarContainer.Has(clientId))
        {
            Debug.Log($"avatar already exists. {clientId}");
            return false;
        }
        
        /*var isNewAvatar = avatarFactory.Invoke(clientId);
        if (!isNewAvatar)
        {
            Debug.Log($"avatarFactory failed. {clientId}");
            return false;
        }*/

        //lifetimeScope.Container.InjectGameObject(newAvatar.gameObject);
        
        /*newAvatar.Initialize(avatarSequence);
        avatarContainer.Add(avatarSequence, newAvatar);*/

        return SpawnPlayer(clientId);
    }
    
    private bool SpawnPlayer(ulong clientId)
    {
        var newGameObject = Object.Instantiate(avatarPrefab, Vector3.zero, Quaternion.identity);
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
        }*/
        
        //var avatarContainer = Container.Resolve<AvatarContainer>();
        //var avatar = newGameObject.GetComponent<Avatar>();
        //avatar.Initialize(avatarSequence);
        //avatarContainer.Add(clientId, avatar);
        
        networkObject.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
        
        return true;
    }
}
