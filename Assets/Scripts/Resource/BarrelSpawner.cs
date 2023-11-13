using System.Collections;
using UnityEngine;
using Utils;

namespace Resource
{
    public class BarrelSpawner : MonoBehaviour
    {
        [SerializeField] private Barrel _barrel;
        [SerializeField] private int _seconds;
        [SerializeField] private int _spawnRadius;
        [SerializeField] private BarrelField _barrelField;
    
        private readonly bool _isActive = true;

        private void Start()
        {
            StartCoroutine(SpawnOnRandomPoint());
        }

        private IEnumerator SpawnOnRandomPoint()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_seconds);

            while (_isActive)
            {
                Vector3 spawnPoint = SupportSpawnerFunction.DefinePointInArea(_barrelField.transform, _spawnRadius);
                Barrel newBarrel = Instantiate(_barrel, spawnPoint, Quaternion.identity);
                _barrelField.AddBarrels(newBarrel);
            
                yield return waitForSeconds;
            }
        
        }
    }
}
