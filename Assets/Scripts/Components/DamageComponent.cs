using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct DamageComponent : IComponentData
    {
        public float Damage;
    }
}
