using Components;
using Unity.Entities;

namespace Systems
{
    public class CollectionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
            
            Entities
                .WithAll<Player>()
                .ForEach((Entity playerEntity, DynamicBuffer<TriggerBuffer> triggerBuffers) =>
                {
                    for (var i = 0; i < triggerBuffers.Length; i++)
                    {
                        var pillEntity = triggerBuffers[i].Entity;
                        if (HasComponent<KillComponent>(pillEntity)) continue;
                        if (HasComponent<Collectable>(pillEntity))
                            entityCommandBuffer.AddComponent(pillEntity, new KillComponent {DelayTimer = 0});

                        if (!HasComponent<PowerPill>(pillEntity)) continue;
                        entityCommandBuffer.AddComponent(playerEntity, GetComponent<PowerPill>(pillEntity));
                        entityCommandBuffer.AddComponent(pillEntity, new KillComponent {DelayTimer = 0});
                    }
                }).Schedule();
            
            Dependency.Complete();
        }
    }
}