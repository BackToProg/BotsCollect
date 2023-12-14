using Environment;
using Outpost;
using UnityEngine;
using Utils;

namespace Units
{
    public class WorkerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointCenter;
        [SerializeField] private int _radius;
        [SerializeField] private Worker _workerTemplate;
        [SerializeField] private WaitingPoint _waitingPointTemplate;
        [SerializeField] private Base _base;
        [SerializeField] private float _positionY = 0.2f;

        public Transform SpawnPointCenter => _spawnPointCenter;
        public WaitingPoint WaitingPointTemplate => _waitingPointTemplate;
        public int Radius => _radius;
        
        private void Awake()
        {
            for (int i = 0; i < _base.WorkersInitialCount; i++)
            {
                SpawnNewWorker();
            }
        }

        public void SpawnNewWorker()
        {
            Vector3 spawnPoint = SupportFunctions.DefinePointInArea(_spawnPointCenter, _radius, _positionY);
            Worker newWorker = Instantiate(_workerTemplate, spawnPoint, Quaternion.identity);
            WaitingPoint newWaitingPoint = Instantiate(_waitingPointTemplate, spawnPoint, Quaternion.identity);
            newWorker.Init(_base, newWaitingPoint);
            _base.AddWorkers(newWorker);
        }

    }
}