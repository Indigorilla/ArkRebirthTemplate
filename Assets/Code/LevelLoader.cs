using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

public class LevelLoader
{
    [Inject] private LevelSettings levelSettings;
    
    private int lastLevelUnique;
    private GameObject lastLevelObject;
    
    public async UniTask<bool> Load(int levelUnique)
    {
        if (lastLevelUnique == levelUnique)
        {
            Debug.Log($"level already loaded. {levelUnique}");
            return false;
        }
        
        var levelData = levelSettings.GetLevelData(levelUnique);
        if (levelData == null)
        {
            Debug.Log($"levelData is null. {levelUnique}");
            return false;
        }
        
        var levelName = levelData.Name;
        var getLevelObject = await Addressables.InstantiateAsync(levelName);
        if (getLevelObject == null)
        {
            Debug.Log($"levelData is null. {levelUnique}, {levelName}");
            return false;
        }

        if (lastLevelObject != null)
        {
            Addressables.ReleaseInstance(lastLevelObject);
        }

        lastLevelObject = getLevelObject;

        return true;
    }
}
