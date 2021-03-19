using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [AlwaysUpdateSystem]
    public class GameStateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var pelletQuery = GetEntityQuery(ComponentType.ReadOnly<Pellet>());
            
            Debug.Log($"Number of pellets left: {pelletQuery.CalculateEntityCount()}");
        }
    }
}