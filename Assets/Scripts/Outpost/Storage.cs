using System.Collections.Generic;
using Environment;
using UnityEngine;
using Utils;

namespace Outpost
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private int _storeRadius;

        private int _barrelCount;
        private List<Barrel> _barrelsOnStock;

        public int BarrelCount => _barrelsOnStock.Count;

        public Vector3 GetPlaceToStore()
        {
            return SupportFunctions.DefinePointInArea(transform, _storeRadius);
        }

        public void AddBarrel(Barrel barrel)
        {
            _barrelsOnStock.Add(barrel);
        }

        public void IssueBarrels()
        {
            foreach (Barrel barrel in _barrelsOnStock)
            {
                barrel.Destroy();
            }
            
            _barrelsOnStock.Clear();
        }
        
        private void Awake()
        {
            _barrelsOnStock = new List<Barrel>();
        }
    }
}