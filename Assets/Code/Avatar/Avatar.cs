using System.Threading;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using Cysharp.Threading.Tasks;
using GOAP;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using UnityEngine;
using VContainer;

public class Avatar : MonoBehaviour
{
    [Inject] private AvatarModelService avatarModelService;
    [Inject] private AvatarSettings avatarSettings;
    [Inject] private AvatarAnimationSettings avatarAnimationSettings;
    [Inject] private GoapBehaviour goap;
    
    private CancellationTokenSource initializeCancellationTokenSource;

    private IMover mover;
    public IMover Mover => mover;
    
    private IAnimator animator;
    public IAnimator Animator => animator;
    
    private AgentBehaviour agentBehaviour;
    private GoapActionProvider actionProvider;
    
    private GameObject modelRoot;
    
    private ClientNetworkAnimator clientNetworkAnimator;

    public void Initialize(ulong clientId)
    {
        gameObject.name = $"Avatar_{clientId}";
        
        mover = gameObject.GetComponent<NavMeshMover>();
        animator = gameObject.GetComponent<AvatarAnimator>();
        
        actionProvider = gameObject.GetComponent<GoapActionProvider>();
        if (actionProvider != null)
        {
            actionProvider.AgentType = goap.GetAgentType("Avatar");    
        }
        
        agentBehaviour = gameObject.GetComponent<AgentBehaviour>();
        if (agentBehaviour != null)
        {
            agentBehaviour.ActionProvider = actionProvider;    
        }
        
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
        
        /*clientNetworkAnimator = gameObject.AddComponent<ClientNetworkAnimator>();
        clientNetworkAnimator.Animator = animator.Animator;*/
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
