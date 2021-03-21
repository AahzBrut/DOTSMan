using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Follow : IComponentData
    {
        public Entity Target;
        public float Distance, MoveSpeed, RotationSpeed;
        public float3 Offset;
        public bool FreezeXPosition, FreezeYPosition, FreezeZPosition;
        public bool FreezeRotation;
    }
}