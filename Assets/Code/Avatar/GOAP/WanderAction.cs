using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GOAP
{
    public class WanderAction : GoapActionBase<AvatarGoapData>
    {
        public override void Start(IMonoAgent agent, AvatarGoapData data)
        {
            data.Timer = Random.Range(1f, 2f);
        }

        public override IActionRunState Perform(IMonoAgent agent, AvatarGoapData data, IActionContext context)
        {
            data.Timer -= context.DeltaTime;

            if (data.Timer > 0f)
            {
                return ActionRunState.Continue;
            }
            
            return ActionRunState.Stop;
        }
    }
}