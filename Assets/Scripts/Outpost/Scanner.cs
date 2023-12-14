using System;
using System.Collections;
using Environment;
using UnityEngine;

namespace Outpost
{
    [RequireComponent(typeof(Base))]
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private int _radius;
        [SerializeField] private LayerMask _layerMask;

        private Base _base;
        private bool _isActive;
        private Coroutine _scanCoroutine;

        private void Awake()
        {
            _base = GetComponent<Base>();
            _isActive = true;
        }

        private void Start()
        {
            _scanCoroutine = StartCoroutine(ScanBarrelField());
        }
        
        private IEnumerator ScanBarrelField()
        {
            while (_isActive)
            {
                Collider[] barrelColliders =
                    Physics.OverlapSphere(_base.BarrelField.transform.position, _radius, _layerMask);
                
                foreach (Collider barrelCollider in barrelColliders)
                {
                    if (!barrelCollider.TryGetComponent(out Barrel barrel)) continue;
                    
                    if (barrel.IsInAction == false)
                    {
                        _base.SendWorkerToCollectBarrel(barrel);
                    }
                }

                yield return null;
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_scanCoroutine);
            _isActive = false;
        }
    }
}