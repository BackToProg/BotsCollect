using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class BarrelField : MonoBehaviour
    {
        private Queue<Barrel> _barrels;
        
        private void Awake()
        {
            _barrels = new Queue<Barrel>();
        }

        public void AddBarrels(Barrel barrel)
        {
            _barrels.Enqueue(barrel);
        }

        public void RemoveBarrelFromField()
        {
            _barrels.Dequeue();
        }

        private Barrel TryGetFreeBarrel()
        {
            foreach (Barrel barrel in _barrels)
            {
                if (barrel.IsInAction == false)
                {
                    return barrel;
                }
            }

            return null;
        }
    }
}