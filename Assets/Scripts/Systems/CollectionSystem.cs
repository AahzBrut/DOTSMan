using Components;
using MonoBeh;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    public class CollectionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
            var addedPoints = new NativeArray<int>(1, Allocator.TempJob);

            Entities
                .WithAll<Player>()
                .ForEach((in Entity playerEntity, in DynamicBuffer<TriggerBuffer> triggerBuffers) =>
                {
                    for (var i = 0; i < triggerBuffers.Length; i++)
                    {
                        var pillEntity = triggerBuffers[i].Entity;
                        if (HasComponent<KillComponent>(pillEntity)) continue;

                        if (HasComponent<Collectable>(pillEntity))
                        {
                            entityCommandBuffer.AddComponent(pillEntity, new KillComponent {DelayTimer = 0});
                            // ReSharper disable once AccessToDisposedClosure
                            addedPoints[0] += GetComponent<Collectable>(pillEntity).points;
                        }

                        if (!HasComponent<PowerPill>(pillEntity)) continue;
                        entityCommandBuffer.AddComponent(playerEntity, GetComponent<PowerPill>(pillEntity));
                        entityCommandBuffer.AddComponent(pillEntity, new KillComponent {DelayTimer = 0});
                    }
                }).Schedule();

            Dependency.Complete();
            if (addedPoints[0] > 0) GameManager.Instance.AddPoints(addedPoints[0]);
            addedPoints.Dispose();
        }
    }
}