using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GOAP
{
    public class AvatarAgentTypeFactory : AgentTypeFactoryBase
    {
        public override IAgentTypeConfig Create()
        {
            var factory = new AgentTypeBuilder("Avatar");
            factory.AddCapability<WanderCapabilityFactory>();
            
            return factory.Build();
        }
    }
}