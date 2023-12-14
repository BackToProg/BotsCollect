using System;
using System.Collections;
using UnityEngine;

namespace Environment
{
    public class BarrelSpawner : MonoBehaviour
    {
        [SerializeField] private Barrel _barrel;
        [SerializeField] private int _seconds;
        [SerializeField] private int _radius;
        [SerializeField] private BarrelField _barrelField;
        [SerializeField] private float _positionY = 0.2f;

        private readonly bool _isActive = true;
        private Coroutine _barrelSpawnCoroutine;

        private void Start()
        {
            _barrelSpawnCoroutine = StartCoroutine(SpawnOnRandomPoint());
        }

        private IEnumerator SpawnOnRandomPoint()
        {
            WaitForSeconds delay = new WaitForSeconds(_seconds);

            while (_isActive)
            {
                Vector3 spawnPoint = Utils.SupportFunctions.DefinePointInArea(_barrelField.transform, _radius, _positionY);
                Barrel newBarrel = Instantiate(_barrel, spawnPoint, Quaternion.identity);
                _barrelField.AddBarrels(newBarrel);

                yield return delay;
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_barrelSpawnCoroutine);
        }
    }
}