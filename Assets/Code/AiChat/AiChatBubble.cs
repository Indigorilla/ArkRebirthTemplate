using System;
using System.Collections;
using TMPro;
using UnityEngine;
using VContainer;

public class AiChatBubble : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text chatText;
    [SerializeField] protected RectTransform rootRect;
    [SerializeField] private RectTransform chatTextRect;
    
    [Inject] private AvatarCameraService avatarCameraService;

    private Transform parent;
    Vector3 offset;
    private Coroutine typeCoroutine;
    
    public void Initialize(Transform parent, Vector3 offset)
    {
        this.parent = parent;
        this.offset = offset;
        
        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
    }

    public void Show(string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            return;
        }
        
        chatText.SetText(msg);
        chatText.ForceMeshUpdate();
        
        chatTextRect.sizeDelta = chatText.GetRenderedValues(false);
        chatText.maxVisibleCharacters = 0;
        
        typeCoroutine = StartCoroutine(PlayTypeEffect(chatText, 0.015f, 2f, 
        () =>
        {
            canvasGroup.alpha = 0f;
        }));
        
        canvasGroup.alpha = 1f;
    }

    private static IEnumerator PlayTypeEffect(TMP_Text text, float typeDelay, float endWaitDelay, Action onClose)
    {
        var totalCharacters = text.textInfo.characterCount;
        var count = 0;
        
        while (true)
        {
            if (count >= totalCharacters)
            {
                break;
            }
            
            text.maxVisibleCharacters = ++count;

            yield return new WaitForSeconds(typeDelay);
        }

        yield return new WaitForSeconds(endWaitDelay);
        
        onClose?.Invoke();
    }

    private void LateUpdate()
    {
        var targetPosition = parent.transform.position;
        var uiPosition = avatarCameraService.WorldPotionToUIPosition(targetPosition);
        uiPosition += offset;
        rootRect.position = uiPosition;
    }
}
