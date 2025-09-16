using System.Collections.Generic;
using UnityEngine;

public class AvatarContainer
{
    private readonly Dictionary<int, Avatar> activeAvatars = new();

    public bool Has(int avatarSequence)
    {
        return activeAvatars.ContainsKey(avatarSequence);
    }
    
    public void Add(int avatarSequence, Avatar avatar)
    {
        if (activeAvatars.TryAdd(avatarSequence, avatar))
        {
            return;
        }
        
        Debug.Log($"avatar already exists. {avatarSequence}");
    }
}
