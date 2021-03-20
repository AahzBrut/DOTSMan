using Components;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public class SpawnSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithStructuralChanges()
                .ForEach((ref Spawner spawner, in Translation translation, in Rotation rotation) =>
                {
                    if (EntityManager.Exists(spawner.SpawnObject)) return;
                    spawner.SpawnObject = EntityManager.Instantiate(spawner.SpawnPrefab);
                    EntityManager.AddComponentData(spawner.SpawnObject, translation);
                    EntityManager.AddComponentData(spawner.SpawnObject, rotation);
                }).Run();
        }
    }
}