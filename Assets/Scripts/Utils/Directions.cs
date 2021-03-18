using Unity.Mathematics;

namespace Utils
{
    public readonly struct Directions
    {
        public static readonly float3[] Values = 
        {
            new float3(0, 0, -1),
            new float3(0, 0, 1),
            new float3(-1, 0, 0),
            new float3(1, 0, 0)
        };
        
        public static readonly float3 Up = Values[0]; 
        public static readonly float3 Down = Values[1]; 
        public static readonly float3 Left = Values[2]; 
        public static readonly float3 Right = Values[3]; 
    }
}