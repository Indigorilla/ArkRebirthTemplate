using UniRx;

public class MainAvatarData
{
    public ReactiveProperty<int> Hp = new(50);
    public ReactiveProperty<int> MaxHp = new(100);
}
