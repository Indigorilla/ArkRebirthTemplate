using UniRx;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class NavMeshMover : MonoBehaviour, IMover
{
    [Inject] private AvatarSettings settings;
    
    private NavMeshAgent navMeshAgent;
    
    public ReactiveProperty<bool> IsMoving { get; set; } = new();
    
    private bool isInitialized;

    public void Initialize()
    {
        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.speed = settings.NavmeshMoverSettings.Speed;
        navMeshAgent.baseOffset = settings.NavmeshMoverSettings.BaseOffset;
        navMeshAgent.angularSpeed = settings.NavmeshMoverSettings.AngularSpeed;
        navMeshAgent.acceleration = settings.NavmeshMoverSettings.Acceleration;
        navMeshAgent.obstacleAvoidanceType = settings.NavmeshMoverSettings.ObstacleAvoidanceType;
        navMeshAgent.autoBraking = settings.NavmeshMoverSettings.AutoBraking;
        navMeshAgent.stoppingDistance = settings.NavmeshMoverSettings.StoppingDistance;
        navMeshAgent.enabled = false;
        isInitialized = true;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (!isInitialized)
        {
            return;
        }
        
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(targetPosition);
        IsMoving.Value = true;
    }

    public void Stop()
    {
        if (!isInitialized)
        {
            return;
        }
        
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        IsMoving.Value = false;
    }

    private void Update()
    {
        if (!isInitialized)
        {
            return;
        }
        
        if (!IsMoving.Value || navMeshAgent == null || navMeshAgent.pathPending)
        {
            return;
        }
        
        CheckStop();
    }

    private bool CheckStop()
    {
        if (navMeshAgent.remainingDistance <= 0.1f)
        {
            if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                var direction = Vector3.Normalize(navMeshAgent.destination - transform.position);
                direction.y = 0f;
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }

                Stop();

                return true;
            }
        }
        
        return false;
    }
}
