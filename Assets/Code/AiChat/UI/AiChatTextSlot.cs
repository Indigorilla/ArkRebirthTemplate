using TMPro;
using UnityEngine;

public class AiChatTextSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public void Initialize(string msg)
    {
        text.text = msg;
        gameObject.SetActive(true);
    }
}
