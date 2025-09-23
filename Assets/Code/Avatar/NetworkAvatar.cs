using Cysharp.Threading.Tasks;
using Unity.Netcode;

public class NetworkAvatar : NetworkBehaviour
{
    private Avatar avatar;
    
    AnimationEnum.Type lastAnimationState;
    
    private readonly NetworkVariable<AnimationEnum.Type> networkAnimationState = new(
        AnimationEnum.Type.Idle,
        writePerm: NetworkVariableWritePermission.Owner);
    
    private void Awake()
    {
        avatar = GetComponent<Avatar>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        AvatarSpawner.Singleton.SpawnAsync(OwnerClientId).Forget();
        
        networkAnimationState.OnValueChanged += HandleNetworkAnimationChanged;
    }
    
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        
        AvatarSpawner.Singleton.Despawn(OwnerClientId);
        
        networkAnimationState.OnValueChanged -= HandleNetworkAnimationChanged;
    }
    
    private void HandleNetworkAnimationChanged(AnimationEnum.Type previousValue, AnimationEnum.Type newValue)
    {
        if (IsOwner)
        {
            return;
        }

        if (previousValue == newValue)
        {
            return;
        }
        
        avatar.Animator.Play(newValue);
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        var currentAnimation = avatar.Animator?.CurrentAnimation ?? AnimationEnum.Type.Idle;
        if (networkAnimationState.Value != currentAnimation)
        {
            networkAnimationState.Value = currentAnimation;
        }
    }
}
