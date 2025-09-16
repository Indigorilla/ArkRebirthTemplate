using System;
using UnityEngine;

[Serializable]
public class AnimationData
{ 
    public AnimationEnum.Type Type;
    public AnimationClip Clip;
}

[CreateAssetMenu(fileName = "AvatarAnimationSettings", menuName = "Settings/AvatarAnimationSettings")]
public class AvatarAnimationSettings : ScriptableObject
{
    [SerializeField] public AnimationData[] Animations;
}