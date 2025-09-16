using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using UniRx;
using VContainer;

public partial class AvatarControllerService
{
    [Inject] MoveTargetMakerService moveTargetMakerService;
    
    private readonly CompositeDisposable disposableBehaviour = new();
    
    private void InitBehaviour()
    {
        agentBehaviour = avatar.GetComponent<AgentBehaviour>();
        agentBehaviour.Events.OnTargetChanged += OnTargetChanged;
        agentBehaviour.Events.OnTargetNotInRange += OnTargetNotInRange;
        
        avatar.Mover.IsMoving.Subscribe(isMoving =>
        {
            var targetAnimationType = isMoving ? AnimationEnum.Type.Run : AnimationEnum.Type.Idle;
            avatar.Animator.Play(targetAnimationType);

            if (targetAnimationType == AnimationEnum.Type.Idle)
            {
                moveTargetMakerService.HideTarget();
            }
        }).AddTo(disposableBehaviour);
    }

    private void ClearBehaviour()
    {
        disposableBehaviour.Clear();
        agentBehaviour.Events.OnTargetChanged -= OnTargetChanged;
        agentBehaviour.Events.OnTargetNotInRange -= OnTargetNotInRange;
    }
    
    private void OnTargetChanged(ITarget target, bool inRange)
    {
        MoveTo(target.Position);
    }
    
    private void OnTargetNotInRange(ITarget target)
    {
        avatar.Mover.Stop();
    }
}
