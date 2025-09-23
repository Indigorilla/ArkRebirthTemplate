using UnityEngine;

[CreateAssetMenu(fileName = "AiChatSetting", menuName = "Settings/AiChatSetting")]
public class AiChatSetting : ScriptableObject
{
    [SerializeField] public string ApiKey;
}
