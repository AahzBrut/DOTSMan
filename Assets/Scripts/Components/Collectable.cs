using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Collectable : IComponentData
    {
        public int points;
    }
}