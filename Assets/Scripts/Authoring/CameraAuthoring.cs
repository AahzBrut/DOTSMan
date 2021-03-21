using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    [DisallowMultipleComponent]
    public class CameraAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField]
        private AudioListener audioListener;

        [SerializeField] 
        private Camera mainCamera;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new CameraTag());
            conversionSystem.AddHybridComponent(mainCamera);
            conversionSystem.AddHybridComponent(audioListener);
        }
    }
}
