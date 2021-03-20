using Unity.Entities;

namespace Components
{
    public struct Spawner : IComponentData
    {
        public Entity SpawnPrefab, SpawnObject;
    }
}