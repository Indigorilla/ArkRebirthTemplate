using UnityEngine;

public interface IAnimator
{
    public AnimationEnum.Type CurrentAnimation { get; }
    public void Initialize();
    public void Play(AnimationEnum.Type type);
}
