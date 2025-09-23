using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class AiChatUiPresenter : MonoBehaviour
{
    [Inject] private AiChatService aiChatService;
    
    [SerializeField] private TMP_InputField chatInputField;
    [SerializeField] private AiChatTextSlot chatTextSlotPrototype;
    
    bool isProgresing = false;
    
    public void Start()
    {
        var submitAction = InputSystem.actions.FindAction("Submit");
        submitAction.performed += OnSubmitAction;
        submitAction.Enable();
    }

    private void OnSubmitAction(InputAction.CallbackContext obj)
    {
        if (isProgresing || string.IsNullOrEmpty(chatInputField.text))
        {
            return;
        }
        
        SendChatAsync(chatInputField.text).Forget();
        
        chatInputField.text = string.Empty;
    }
    
    private async UniTask SendChatAsync(string msg)
    {
        isProgresing = true;
        var resultString = await aiChatService.SendChat(msg);

        if (string.IsNullOrEmpty(resultString))
        {
            isProgresing = false;
            return;
        }
        
        var newChatTextSlot = Instantiate(chatTextSlotPrototype, chatTextSlotPrototype.transform.parent);
        newChatTextSlot.Initialize(resultString);
        
        isProgresing = false;
    }
}
