using Resource;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace BaseFunctions
{
    public class Storage: MonoBehaviour
    {
        [SerializeField] private int _storeRadius;

        private int _barrelCount;

        public void IncreaseBarrelCount()
        {
            _barrelCount++;
        }
        
        public Vector3 GetPlaceToStore()
        {
            return SupportFunction.DefinePointInArea(transform, _storeRadius);
        }
    }
}