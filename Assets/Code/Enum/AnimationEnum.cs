using System;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationEnum
{
    public enum Type
    {
        Idle = 1,
        Run = 2
    }
    
    public static readonly Dictionary<Type, int> TypeToHash = new();
    
    public static void Initialize()
    {
        foreach (var animType in Enum.GetValues(typeof(Type)))
        {
            var animName = animType.ToString();
            TypeToHash.Add((Type) animType, Animator.StringToHash(animName));
        }
    }
}
