using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace BaseFunctions
{
    public class Storage: MonoBehaviour
    {
        [SerializeField] private int _storeRadius;

        private Vector3 _freePlace;
        
        public Vector3 GetPlaceToStore()
        {
            return SupportSpawnerFunction.DefinePointInArea(transform, _storeRadius);
        }
    }
}