using System.Threading;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using Cysharp.Threading.Tasks;
using GOAP;
using UnityEngine;
using VContainer;

public class Avatar : MonoBehaviour
{
    [Inject] private AvatarModelService avatarModelService;
    [Inject] private AvatarSettings avatarSettings;
    [Inject] private AvatarAnimationSettings avatarAnimationSettings;
    [Inject] private GoapBehaviour goap;
    
    private int avatarSequence;
    private CancellationTokenSource initializeCancellationTokenSource;

    private IMover mover;
    public IMover Mover => mover;
    
    private IAnimator animator;
    public IAnimator Animator => animator;
    
    private AgentBehaviour agentBehaviour;
    private GoapActionProvider actionProvider;
    
    private GameObject modelRoot;

    public void Initialize(int avatarSequence)
    {
        this.avatarSequence = avatarSequence;
        
        mover = gameObject.GetComponent<NavMeshMover>();
        animator = gameObject.GetComponent<AvatarAnimator>();
        
        actionProvider = gameObject.GetComponent<GoapActionProvider>();
        actionProvider.AgentType = goap.GetAgentType("Avatar");
        
        agentBehaviour = gameObject.GetComponent<AgentBehaviour>();
        agentBehaviour.ActionProvider = actionProvider;
        
        initializeCancellationTokenSource = new CancellationTokenSource();
        StartAsync(initializeCancellationTokenSource.Token).Forget();
    }
    
    public void MoveTo(Vector3 targetPosition)
    {
        mover.MoveTo(targetPosition);
    }

    public void SetWanderingMode(bool isWanderingMode)
    {
        if (isWanderingMode)
        {
            actionProvider.RequestGoal<WanderGoal>();
        }
        else
        {
            actionProvider.RequestGoal<EmptyGoal>();
        }
    }
    
    private async UniTask StartAsync(CancellationToken cancellation)
    {
        var modelName = avatarSettings.ModelName;
        modelRoot = await avatarModelService.GetModel(modelName, transform);
        if (modelRoot == null)
        {
            Debug.Log($"modelObject is null. {modelName}");
        }
        
        if (cancellation.IsCancellationRequested)
        {
            Debug.Log($"IsCancellationRequested. {modelName}");
            return;
        }
        
        mover.Initialize();
        animator.Initialize();
    }

    private void OnDestroy()
    {
        if (modelRoot != null)
        {
            Destroy(modelRoot);
            modelRoot = null;    
        }
    }
}
