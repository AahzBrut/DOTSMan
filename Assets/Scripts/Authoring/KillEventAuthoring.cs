using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    [DisallowMultipleComponent]
    public class KillEventAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        [SerializeField]
        private string sfxName;
        
        [SerializeField]
        private GameObject spawnPrefab;
        
        [SerializeField]
        private int pointValue;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new KillEvent
            {
                ClipName = sfxName, 
                SpawnPrefab = conversionSystem.GetPrimaryEntity(spawnPrefab), 
                PointValue = pointValue
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(spawnPrefab);
        }
    }
}
