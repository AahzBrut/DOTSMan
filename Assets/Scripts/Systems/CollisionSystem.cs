using System;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using Collision = Components.Collision;

namespace Systems
{
    public class CollisionSystem : SystemBase
    {
        private struct CollisionSystemJob : ICollisionEventsJob
        {
            public BufferFromEntity<Collision> Collisions;

            public void Execute(CollisionEvent collisionEvent)
            {
                Debug.Log("In CollisionSystemJob.Execute()");
                if (Collisions.HasComponent(collisionEvent.EntityA))
                    Collisions[collisionEvent.EntityA].Add(new Collision {Entity = collisionEvent.EntityB});
                if (Collisions.HasComponent(collisionEvent.EntityB))
                    Collisions[collisionEvent.EntityB].Add(new Collision {Entity = collisionEvent.EntityA});
            }
        }

        private struct TriggerSystemJob : ITriggerEventsJob
        {
            public void Execute(TriggerEvent triggerEvent)
            {
                throw new NotImplementedException();
            }
        }

        protected override void OnUpdate()
        {
            Debug.Log("In CollisionSystem.OnUpdate()");
            var physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
            var simulation = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;

            var jobHandle = new CollisionSystemJob
            {
                Collisions = GetBufferFromEntity<Collision>()
            }.Schedule(simulation, ref physicsWorld, Dependency);
        }
    }
}