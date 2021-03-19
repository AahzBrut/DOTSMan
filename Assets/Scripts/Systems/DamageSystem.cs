using Components;
using Unity.Entities;

namespace Systems
{
    public class DamageSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;

            var entityCommandBuffer =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();

            ProcessCollisions();
            InflictDamage(deltaTime, entityCommandBuffer);
            KillDeadEntities(deltaTime, entityCommandBuffer);
        }

        private void KillDeadEntities(float deltaTime, EntityCommandBuffer entityCommandBuffer)
        {
            Entities.ForEach((Entity entity, ref KillComponent killComponent) =>
            {
                killComponent.DelayTimer -= deltaTime;
                if (killComponent.DelayTimer <= 0)
                    entityCommandBuffer.DestroyEntity(entity);
            }).Schedule();
        }

        private void InflictDamage(float deltaTime, EntityCommandBuffer entityCommandBuffer)
        {
            Entities
                .WithNone<KillComponent>()
                .ForEach((Entity entity, ref HealthComponent healthComponent) =>
                {
                    healthComponent.InvincibleTimer -= deltaTime;
                    if (healthComponent.Health > 0) return;
                    entityCommandBuffer.AddComponent(entity,
                        new KillComponent {DelayTimer = healthComponent.KillTimer});
                }).Schedule();
        }

        private void ProcessCollisions()
        {
            Entities.ForEach((DynamicBuffer<CollisionBuffer> collisionBuffers, ref HealthComponent healthComponent) =>
            {
                for (var i = 0; i < collisionBuffers.Length; i++)
                {
                    if (!(healthComponent.InvincibleTimer <= 0) ||
                        !HasComponent<DamageComponent>(collisionBuffers[i].Entity)) continue;

                    healthComponent.Health -= GetComponent<DamageComponent>(collisionBuffers[i].Entity).Damage;
                    healthComponent.InvincibleTimer = 1;
                }
            }).Schedule();
        }
    }
}