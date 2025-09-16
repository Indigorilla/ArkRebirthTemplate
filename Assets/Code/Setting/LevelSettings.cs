using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int LevelUnique;
    public string Name;
}

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] public LevelData[] Levels;
    
    public LevelData GetLevelData(int levelUnique)
    {
        return Levels.FirstOrDefault(x => x.LevelUnique == levelUnique);
    }
}
