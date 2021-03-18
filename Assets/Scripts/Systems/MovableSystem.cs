using Components;
using Unity.Entities;
using Unity.Physics;

namespace Systems
{
    public class MovableSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PhysicsVelocity velocity, in Movable mov) =>
            {
                var step = mov.Direction * mov.Speed;
                velocity.Linear = step;
            }).Schedule();
        }
    }
}
