using System.Collections.Generic;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class AvatarSpawner : NetworkBehaviour
{
    [Inject] private Avatar avatarPrefab;
    [SerializeField] private GameLifetimeScope lifetimeScope;

    private static AvatarSpawner instance;
    public static AvatarSpawner Singleton => instance;
    
    public void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        NetworkManager.Singleton?.Shutdown();
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnConnectionEvent += OnConnectionEvent;
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneLoaded;  //Host 시작시
        NetworkManager.Singleton.SceneManager.OnSynchronizeComplete += OnSynchronizeComplete; //Host 시작 후 피어 접속 처리

        if (IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Single);    
        }
    }

    private void OnConnectionEvent(NetworkManager networkManager, ConnectionEventData connectionEventData)
    {
        Debug.Log($"OnConnectionEvent {connectionEventData.ClientId}, {connectionEventData.EventType}");
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted -= SceneLoaded;
        NetworkManager.Singleton.SceneManager.OnSynchronizeComplete -= OnSynchronizeComplete;
    }

    private void OnSynchronizeComplete(ulong clientId)
    {
        if (!IsHost)
        {
            return;
        }

        var newAvatar = Instantiate(avatarPrefab, Vector3.zero, Quaternion.identity);
        newAvatar.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }
    
    private void SceneLoaded(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (!IsHost || sceneName != "Game")
        {
            return;
        }

        foreach (var id in clientsCompleted)
        {
            var newAvatar = Instantiate(avatarPrefab, Vector3.zero, Quaternion.identity);
            newAvatar.GetComponent<NetworkObject>().SpawnAsPlayerObject(id, true);
        }
    }
    
    public async UniTask SpawnAsync(ulong clientId)
    {
        var avatarContainer = lifetimeScope.Container.Resolve<AvatarContainer>();
        if (avatarContainer.Has(clientId))
        {
            return;
        }

        Avatar avatar = null;
        NetworkObject playerObject = null;
        while (avatar == null)
        {
            playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            if (playerObject != null)
            {
                avatar = playerObject.GetComponent<Avatar>();    
            }
            
            await UniTask.Yield();
        }
        
        lifetimeScope.Container.InjectGameObject(playerObject.gameObject);
        
        if (playerObject.IsOwner)
        {
            var playerGameObject = playerObject.gameObject;
            playerGameObject.AddComponent<GoapActionProvider>();
            playerGameObject.AddComponent<AgentBehaviour>();
        }
        
        avatar.Initialize(clientId);
        
        avatarContainer.Add(clientId, avatar);
    }

    public void Despawn(ulong clientId)
    {
        var avatarContainer = lifetimeScope.Container.Resolve<AvatarContainer>();
        if (!avatarContainer.Has(clientId))
        {
            return;
        }
        
        avatarContainer.Remove(clientId);
    }
}
