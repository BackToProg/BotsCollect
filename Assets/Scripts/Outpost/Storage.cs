using System.Collections.Generic;
using Environment;
using UnityEngine;
using Utils;

namespace Outpost
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private int _storeRadius;
        [SerializeField] private float _positionY = 0.2f;

        private int _barrelCount;
        private List<Barrel> _barrelsOnStock;

        public int BarrelCount => _barrelsOnStock.Count;

        private void Awake()
        {
            _barrelsOnStock = new List<Barrel>();
        }

        public Vector3 GetPlaceToStore()
        {
            return SupportFunctions.DefinePointInArea(transform, _storeRadius, _positionY);
        }

        public void AddBarrel(Barrel barrel)
        {
            _barrelsOnStock.Add(barrel);
        }

        public void IssueBarrels(int issueBarrelCount)
        {
            for (int i = 0; i < issueBarrelCount; i++)
            {
                _barrelsOnStock[i].Destroy();
            }

            _barrelsOnStock.RemoveRange(0, issueBarrelCount);
        }
    }
}