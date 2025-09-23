using UniRx;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIPresenter : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    
    private CompositeDisposable lobbyUIDisposable = new();
    
    public void Start()
    {
        hostButton.OnClickAsObservable().Subscribe(_ =>
        {
            NetworkManager.Singleton.StartHost();
            Clear();
        }).AddTo(lobbyUIDisposable);
        
        clientButton.OnClickAsObservable().Subscribe(_ =>
        {
            NetworkManager.Singleton.StartClient();
            Clear();
        }).AddTo(lobbyUIDisposable);
    }

    private void Clear()
    {
        lobbyUIDisposable.Clear();
        gameObject.SetActive(false);
    }
}
