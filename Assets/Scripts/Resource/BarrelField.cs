using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Resource
{
    public class BarrelField: MonoBehaviour
    {
        private List<Barrel> _barrels;

        public List<Barrel> Barrels => _barrels;

        public void AddBarrels(Barrel barrel)
        {
            _barrels.Add(barrel);
        }
        
        private void Awake()
        {
            _barrels = new List<Barrel>();
        }
    }
}