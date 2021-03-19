using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct HealthComponent : IComponentData
    {
        public float Health, InvincibleTimer, KillTimer;
    }
}
