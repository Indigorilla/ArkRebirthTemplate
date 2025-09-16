using CrashKonijn.Agent.Core;

namespace GOAP
{
    public class AvatarGoapData : IActionData
    {
        public ITarget Target { get; set; }
        public float Timer { get; set; }
    }
}