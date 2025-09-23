using UnityEngine;

public class AiChatAgent : MonoBehaviour
{
    [SerializeField] private int id;
    
    public int Id => id;
    
    private AiChatBubble aiChatBubble;

    public void Initialize(AiChatBubble aiChatBubble)
    {
        this.aiChatBubble = aiChatBubble;
    }

    public void ShowChat(string msg)
    {
        aiChatBubble.Show(msg);
    }
}
