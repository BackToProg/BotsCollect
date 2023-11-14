using System.Collections.Generic;
using Resource;
using UnityEngine;
using Utils;

namespace BaseFunctions
{
    public class BaseScanner : MonoBehaviour
    {
       [SerializeField] private BarrelField _barrelField;
        
        public Barrel ScanArea()
        {
            foreach (var barrel in _barrelField.Barrels)
            {
                if (IsBarrelStateIdle(barrel))
                {
                    return barrel;
                }
            }

            return null;
        }

        private bool IsBarrelStateIdle(Barrel barrel) => barrel.BarrelState == BarrelState.Idle;
    }
}