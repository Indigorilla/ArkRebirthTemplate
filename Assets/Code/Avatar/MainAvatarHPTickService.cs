using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainAvatarHPTickService : ITickable
{
    [Inject] MainAvatarData mainAvatarData;
    
    double lastTickTime;
    
    public void Tick()
    {
        if (Time.timeAsDouble - lastTickTime < 1f)
        {
            return;
        }

        lastTickTime = Time.timeAsDouble;
        mainAvatarData.Hp.Value = Mathf.Min(mainAvatarData.Hp.Value + 1, mainAvatarData.MaxHp.Value);
    }
}
