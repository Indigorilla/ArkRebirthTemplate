using UniRx;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIPresenter : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    //[SerializeField] private Button startGame;
    
    private CompositeDisposable lobbyUIDisposable = new();
    
    public void Start()
    {
        hostButton.OnClickAsObservable().Subscribe(_ =>
        {
            NetworkManager.Singleton.StartHost();
            //OffUI();
        }).AddTo(lobbyUIDisposable);
        
        clientButton.OnClickAsObservable().Subscribe(_ =>
        {
            NetworkManager.Singleton.StartClient();
            //OffUI();
        }).AddTo(lobbyUIDisposable);
    }

    public void OffUI()
    {
        lobbyUIDisposable.Clear();
        gameObject.SetActive(false);
    }
}
