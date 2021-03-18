using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using Utils;

namespace Systems
{
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class EnemySystem : SystemBase
    {
        private Random _rng = new Random(1425);

        protected override void OnUpdate()
        {
            var physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
            _rng.NextInt();
            var rng = _rng;

            Entities.ForEach((ref Movable mov, ref Enemy enemy, in Translation translation) =>
            {
                if (!(math.distance(translation.Value, enemy.PrevPosition) > .95f)) return;
                enemy.PrevPosition = math.round(translation.Value);

                var validDirections = new NativeList<float3>(Allocator.Temp);
                for (var i = 0; i < 4; i++)
                {
                    if (Directions.Values[i].Equals(-mov.Direction)) continue;
                    if (!physicsWorld.CastRay(GetRaycastInput(translation.Value, Directions.Values[i])))
                        validDirections.Add(Directions.Values[i]);
                }

                mov.Direction = validDirections[rng.NextInt(validDirections.Length)];

                validDirections.Dispose();
            }).Schedule();
            Dependency.Complete();
        }

        private static RaycastInput GetRaycastInput(in float3 position, in float3 direction)
        {
            var ray = new RaycastInput
            {
                Start = position,
                End = position + direction * .9f,
                Filter = new CollisionFilter
                {
                    GroupIndex = 0,
                    BelongsTo = 1u << 1,
                    CollidesWith = 1u << 2
                }
            };
            return ray;
        }
    }
}