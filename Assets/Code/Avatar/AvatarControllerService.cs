using System.Collections.Generic;
using CrashKonijn.Agent.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;
using UniRx;

public partial class AvatarControllerService
{
    [Inject] private Camera mainCamera;
    
    private InputAction moveAction;
    private InputAction screenPositionAction;
    
    private Avatar avatar;
    private AgentBehaviour agentBehaviour;
    private bool isPossession;
    
    public ReactiveProperty<bool> IsWanderingMode { get; } = new(false);
    
    public void Possession(Avatar avatar)
    {
        if (this.avatar != null)
        {
            UnPossession();
        }
        
        this.avatar = avatar;
        
        moveAction = InputSystem.actions.FindAction("MoveTo");
        moveAction.performed += OnMoveToInput;
        moveAction.Enable();
        
        screenPositionAction = InputSystem.actions.FindAction("ScreenPosition");
        screenPositionAction.Enable();

        InitBehaviour();
        
        isPossession = true;
    }

    public void UnPossession()
    {
        ClearBehaviour();
        
        IsWanderingMode.Value = false;
        
        isPossession = false;
        
        screenPositionAction.Disable();
        
        moveAction.performed -= OnMoveToInput;
        moveAction.Disable();
        
        avatar = null;
    }

    private void MoveTo(Vector3 targetPosition)
    {
        avatar.MoveTo(targetPosition);
        moveTargetMakerService.SetTarget(targetPosition);
    }

    public void SetWanderingMode(bool isWanderingMode)
    {
        if (!isPossession)
        {
            return;
        }
        
        IsWanderingMode.Value = isWanderingMode;
        avatar.SetWanderingMode(isWanderingMode);
    }

    private void OnMoveToInput(InputAction.CallbackContext context)
    {
        if (!isPossession)
        {
            return;
        }
        
        var screenPosition = screenPositionAction.ReadValue<Vector2>();
        if (IsPointerOverUIObject(screenPosition))
        {
            return;
        }
        
        var ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out var hit))
        {
            MoveTo(hit.point);
            IsWanderingMode.Value = false;
        }
    }
    
    private static bool IsPointerOverUIObject(Vector2 touchPos)
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = touchPos
        };

        var results = new List<RaycastResult>();
        
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
 		
        return results.Count > 0;
    }
}
