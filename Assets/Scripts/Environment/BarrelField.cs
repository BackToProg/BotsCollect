using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Environment
{
    public class BarrelField : MonoBehaviour
    {
        private Queue<Barrel> _barrels;

        public event EventHandler<Barrel> OnFreeBarrel;

        public void AddBarrels(Barrel barrel)
        {
            _barrels.Enqueue(barrel);
        }

        public void RemoveBarrelFromField()
        {
            _barrels.Dequeue();
        }

        private void Update()
        {
            StartCoroutine(CreateEventForNewBarrel());
        }

        private void Awake()
        {
            _barrels = new Queue<Barrel>();
        }

        private IEnumerator CreateEventForNewBarrel()
        {
            while (_barrels.Count > 0)
            {
                Barrel barrel = TryGetFreeBarrel();

                if (barrel != null)
                {
                    OnFreeBarrel?.Invoke(this, barrel);
                }

                yield return null;
            }
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