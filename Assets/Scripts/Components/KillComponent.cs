using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct KillComponent : IComponentData
    {
        public float DelayTimer;
    }
}