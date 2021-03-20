using Components;
using MonoBeh;
using Unity.Entities;

namespace Systems
{
    [AlwaysUpdateSystem]
    public class GameStateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var pelletQuery = GetEntityQuery(ComponentType.ReadOnly<Pellet>());

            if (pelletQuery.CalculateEntityCount() <= 0)
                GameManager.Instance.Win();

            var playerQuery = GetEntityQuery(ComponentType.ReadOnly<Player>());
            
            if (playerQuery.CalculateEntityCount() <= 0)
                GameManager.Instance.Loose();

        }
    }
}