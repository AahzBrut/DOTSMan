using Components;
using Unity.Entities;

namespace Systems
{
    public class InvincibilityService : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();

            var deltaTime = Time.DeltaTime;

            Entities
                .WithAll<Player, PowerPill>()
                .ForEach((
                    Entity playerEntity,
                    ref HealthComponent healthComponent,
                    ref DamageComponent damageComponent,
                    ref PowerPill powerPill) =>
                {
                    powerPill.PillTimeOut -= deltaTime;
                    healthComponent.InvincibleTimer = powerPill.PillTimeOut;
                    damageComponent.Damage = 1f;
                    
                    if (healthComponent.InvincibleTimer > 0) return;
                    
                    entityCommandBuffer.RemoveComponent<PowerPill>(playerEntity);
                    damageComponent.Damage = 0f;
                }).Schedule();
            
            Dependency.Complete();
        }
    }
}