using Unity.Collections;
using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct KillEvent : IComponentData
    {
        public FixedString32 ClipName;
        public Entity SpawnPrefab;
        public int PointValue;
    }
}