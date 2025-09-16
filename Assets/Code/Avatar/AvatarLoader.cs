using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class AvatarLoader
{
    [Inject] private GameLifetimeScope lifetimeScope;
    [Inject] private Func<int, Avatar> avatarFactory;
    [Inject] private AvatarContainer avatarContainer;
    
    public Avatar Load(int avatarSequence)
    {
        if (avatarContainer.Has(avatarSequence))
        {
            Debug.Log($"avatar already exists. {avatarSequence}");
            return null;
        }
        
        var newAvatar = avatarFactory.Invoke(avatarSequence);
        if (newAvatar == null)
        {
            Debug.Log($"avatarFactory failed. {avatarSequence}");
            return null;
        }

        lifetimeScope.Container.InjectGameObject(newAvatar.gameObject);
        
        newAvatar.Initialize(avatarSequence);
        avatarContainer.Add(avatarSequence, newAvatar);

        return newAvatar;
    }
}
