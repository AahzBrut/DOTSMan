using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    [DisallowMultipleComponent]
    public class SpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject spawnObject;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new Spawner
                {
                    SpawnPrefab = conversionSystem.GetPrimaryEntity(spawnObject)
                });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(spawnObject);
        }
    }
}