using UnityEngine;
using Utils;

namespace Outpost
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private int _storeRadius;

        private int _barrelCount;

        public void IncreaseStoreBarrelCount()
        {
            _barrelCount++;
        }

        public Vector3 GetPlaceToStore()
        {
            return SupportFunctions.DefinePointInArea(transform, _storeRadius);
        }
    }
}