using System;
using System.Collections.Generic;
using Resource;
using Unity.VisualScripting;
using UnityEngine;

namespace BaseFunctions
{
    public class BaseScanner : MonoBehaviour
    {
        public Barrel ScanArea(List<Barrel> barrels)
        {
            foreach (var barrel in barrels)
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