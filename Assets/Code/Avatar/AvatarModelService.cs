using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AvatarModelService
{
    private readonly Dictionary<string, GameObject> loadedAvatarModels = new();
    
    public async UniTask<GameObject> GetModel(string name, Transform parent)
    {
        if (loadedAvatarModels.TryGetValue(name, out var model))
        {
            return GameObject.Instantiate(model, parent);
        }
        
        var loadedModel = await Addressables.LoadAssetAsync<GameObject>(name);
        if (loadedModel == null)
        {
            Debug.Log($"model load failed. {name}");
            return null;
        }
        
        loadedAvatarModels.TryAdd(name, loadedModel);
        
        var modelInstance = GameObject.Instantiate(loadedModel, parent);
        return modelInstance;
    }
    
    public void Clean()
    {
        loadedAvatarModels.Clear();
        Addressables.CleanBundleCache();
    }
}
