using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class AvatarAnimator : MonoBehaviour, IAnimator
{
    [Inject] private AvatarAnimationSettings settings;
    
    private Animator animator;
    private readonly Dictionary<AnimationEnum.Type, float> aniClipLength = new();
    private bool isInitialize;
    
    public void Initialize()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        animator.applyRootMotion = false;
        
        var overrideRuntimeAnimatorController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideRuntimeAnimatorController;
        
        var overrideRuntimeClip = new AnimationClipOverrides(overrideRuntimeAnimatorController.overridesCount);
        overrideRuntimeAnimatorController.GetOverrides(overrideRuntimeClip);
        
        SetOverrideClip(overrideRuntimeClip);
        
        overrideRuntimeAnimatorController.ApplyOverrides(overrideRuntimeClip);

        isInitialize = true;
    }
    
    public void Play(AnimationEnum.Type type)
    {
        if (!isInitialize)
        {
            return;
        }
        
        var hash = AnimationEnum.TypeToHash.GetValueOrDefault(type);
        if (hash == 0)
        {
            return;
        }
        
        animator.CrossFade(hash, 0.1f);
    }
    
    private void SetOverrideClip(AnimationClipOverrides clipOverrides)
    {
        var anims = settings.Animations;
        foreach (var ani in anims)
        {
            var typeName = ani.Type.ToString();
            clipOverrides[typeName] = ani.Clip;
            
            var clipLength = ani.Clip.length;
            aniClipLength[ani.Type] = clipLength;
        }
    }
}
