using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GOAP
{
    public class WanderCapabilityFactory : CapabilityFactoryBase
    {
        public override ICapabilityConfig Create()
        {
            var builder = new CapabilityBuilder("AvatarSet");

            builder.AddGoal<WanderGoal>().AddCondition<IsWander>(Comparison.GreaterThanOrEqual, 1);
            builder.AddGoal<EmptyGoal>().AddCondition<IsWander>(Comparison.SmallerThan, 1);

            builder.AddAction<WanderAction>().SetTarget<WanderTarget>().AddEffect<IsWander>(EffectType.Increase)
                .SetBaseCost(5).SetStoppingDistance(10);

            builder.AddTargetSensor<WanderSensor>().SetTarget<WanderTarget>();
            
            return builder.Build();
        }
    }
}