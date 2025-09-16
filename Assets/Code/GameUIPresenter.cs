using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class GameUIPresenter : MonoBehaviour
{
    [Inject] private MainAvatarData mainAvatarData;
    [Inject] private AvatarControllerService avatarControllerService;
    
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Toggle wanderingModeToggle;
    
    private readonly CompositeDisposable disposable = new();
    
    public void Start()
    {
        mainAvatarData.MaxHp.Subscribe(SetMaxHp).AddTo(disposable);
        mainAvatarData.Hp.Subscribe(SetHp).AddTo(disposable);

        wanderingModeToggle.OnValueChangedAsObservable().Subscribe(isOn =>
        {
            avatarControllerService.SetWanderingMode(isOn);
        }).AddTo(disposable);

        avatarControllerService.IsWanderingMode.Subscribe(isOn =>
        {
            wanderingModeToggle.isOn = isOn;
        }).AddTo(disposable);
    }
    
    private void SetHp(int hp)
    {
        hpSlider.value = hp;
        hpText.text = $"{hp}/{mainAvatarData.MaxHp.Value}";
    }
    
    private void SetMaxHp(int maxHp)
    {
        hpSlider.maxValue = maxHp;
        hpText.text = $"{mainAvatarData.Hp.Value}/{maxHp}";
    }
}
