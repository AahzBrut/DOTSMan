using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct PowerPill : IComponentData
    {
        public float PillTimeOut;
    }
}