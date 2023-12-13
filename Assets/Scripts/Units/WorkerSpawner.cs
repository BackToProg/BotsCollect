using Environment;
using Outpost;
using UnityEngine;
using Utils;

namespace Units
{
    public class WorkerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointArea;
        [SerializeField] private int _spawnRadius;
        [SerializeField] private Worker _workerTemplate;
        [SerializeField] private WaitingPoint _waitingPointTemplate;
        [SerializeField] private Base _base;

        public Transform SpawnPointArea => _spawnPointArea;
        public WaitingPoint WaitingPointTemplate => _waitingPointTemplate;
        public int SpawnRadius => _spawnRadius;

        public void SpawnNewWorker()
        {
            Vector3 spawnPoint = SupportFunctions.DefinePointInArea(_spawnPointArea, _spawnRadius);
            Worker newWorker = Instantiate(_workerTemplate, spawnPoint, Quaternion.identity);
            WaitingPoint newWaitingPoint = Instantiate(_waitingPointTemplate, spawnPoint, Quaternion.identity);
            newWorker.Init(_base, newWaitingPoint);
            _base.AddWorkers(newWorker);
        }
        
        private void Awake()
        {
            for (int i = 0; i < _base.WorkersInitialCount; i++)
            {
                SpawnNewWorker();
            }
        }
    }
}