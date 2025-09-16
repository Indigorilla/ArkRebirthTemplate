using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public class WanderSensor : LocalTargetSensorBase
    {
        public override void Created() { }

        public override void Update() { }

        public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
        {
            var targetPosition = GetRandomPosition(agent);
            
            return new PositionTarget(targetPosition);
        }
        
        private Vector3 GetRandomPosition(IActionReceiver agent)
        {
            var count = 0;

            var originPosition = agent.Transform.position;
            
            while (count < 5)
            {
                var random = Random.insideUnitCircle * 5f;
                var position = originPosition + new Vector3(random.x, 0, random.y);
                if (NavMesh.SamplePosition(position, out var hit, 1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }

                ++count;
            }

            return originPosition;
        }
    }
}