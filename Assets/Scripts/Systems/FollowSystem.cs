using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class FollowSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;

            Entities
                .WithAll<Translation, Rotation>()
                .ForEach((in Entity entity, in Follow follow) =>
                {
                    if (!HasComponent<Translation>(follow.Target) || !HasComponent<Rotation>(follow.Target)) return;
                    var position = GetComponent<Translation>(entity).Value;
                    var rotation = GetComponent<Rotation>(entity).Value;
                    var targetPosition = GetComponent<Translation>(follow.Target).Value;
                    var targetRotation = GetComponent<Rotation>(follow.Target).Value;
                    
                    targetPosition += math.mul(targetRotation, targetPosition) * -follow.Distance;
                    targetPosition += follow.Offset;

                    targetPosition.x = follow.FreezeXPosition ? position.x : targetPosition.x; 
                    targetPosition.y = follow.FreezeYPosition ? position.y : targetPosition.y; 
                    targetPosition.z = follow.FreezeZPosition ? position.z : targetPosition.z;
                    targetRotation = follow.FreezeRotation ? rotation : targetRotation;
                    
                    targetPosition = math.lerp(position, targetPosition, dt * follow.MoveSpeed);
                    targetRotation = math.lerp(rotation.value, targetRotation.value, dt * follow.RotationSpeed);
                    
                    SetComponent(entity, new Translation {Value = targetPosition});
                    SetComponent(entity, new Rotation {Value = targetRotation});
                }).Schedule();
        }
    }
}