using System.Collections.Generic;
using UnityEngine;

public class AvatarContainer
{
    private readonly Dictionary<ulong, Avatar> activeAvatars = new();

    public bool Has(ulong clientID)
    {
        return activeAvatars.ContainsKey(clientID);
    }

    public Avatar Get(ulong clientId)
    {
        return activeAvatars.GetValueOrDefault(clientId);
    }
    
    public void Add(ulong clientId, Avatar avatar)
    {
        if (activeAvatars.TryAdd(clientId, avatar))
        {
            return;
        }
        
        Debug.Log($"avatar already exists. {clientId}");
    }
    
    public void Remove(ulong clientId)
    {
        activeAvatars.Remove(clientId);
    }
}
