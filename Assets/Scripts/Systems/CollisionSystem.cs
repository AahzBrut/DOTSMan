using Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{
    public class CollisionSystem : SystemBase
    {
        private struct CollisionSystemJob : ICollisionEventsJob
        {
            public BufferFromEntity<CollisionBuffer> Collisions;

            public void Execute(CollisionEvent collisionEvent)
            {
                if (Collisions.HasComponent(collisionEvent.EntityA))
                    Collisions[collisionEvent.EntityA].Add(new CollisionBuffer {Entity = collisionEvent.EntityB});
                if (Collisions.HasComponent(collisionEvent.EntityB))
                    Collisions[collisionEvent.EntityB].Add(new CollisionBuffer {Entity = collisionEvent.EntityA});
            }
        }

        private struct TriggerSystemJob : ITriggerEventsJob
        {
            public BufferFromEntity<TriggerBuffer> Triggers;

            public void Execute(TriggerEvent triggerEvent)
            {
                if (Triggers.HasComponent(triggerEvent.EntityA))
                    Triggers[triggerEvent.EntityA].Add(new TriggerBuffer {Entity = triggerEvent.EntityB});
                if (Triggers.HasComponent(triggerEvent.EntityB))
                    Triggers[triggerEvent.EntityB].Add(new TriggerBuffer {Entity = triggerEvent.EntityA});
            }
        }

        protected override void OnUpdate()
        {
            var physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
            var simulation = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;

            HandleCollisions(simulation, physicsWorld);
            HandleTriggers(simulation, physicsWorld);
            Dependency.Complete();
        }

        private void HandleTriggers(ISimulation simulation, PhysicsWorld physicsWorld)
        {
            Entities.ForEach((DynamicBuffer<TriggerBuffer> triggers) => { triggers.Clear(); }).Run();

            Dependency = new TriggerSystemJob
            {
                Triggers = GetBufferFromEntity<TriggerBuffer>()
            }.Schedule(simulation, ref physicsWorld, Dependency);
        }

        private void HandleCollisions(ISimulation simulation, PhysicsWorld physicsWorld)
        {
            Entities.ForEach((DynamicBuffer<CollisionBuffer> collisions) => { collisions.Clear(); }).Run();

            Dependency = new CollisionSystemJob
            {
                Collisions = GetBufferFromEntity<CollisionBuffer>()
            }.Schedule(simulation, ref physicsWorld, Dependency);
        }
    }
}