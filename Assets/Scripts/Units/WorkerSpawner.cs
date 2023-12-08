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
        [SerializeField] private Worker _worker;
        [SerializeField] private WaitingPoint _waitingPoint;
        [SerializeField] private Base _base;

        private void Awake()
        {
            for (int i = 0; i < _base.WorkersCount; i++)
            {
                Vector3 spawnPoint = SupportFunctions.DefinePointInArea(_spawnPointArea, _spawnRadius);
                Worker newWorker = Instantiate(_worker, spawnPoint, Quaternion.identity);
                WaitingPoint newWaitingPoint = Instantiate(_waitingPoint, spawnPoint, Quaternion.identity);
                newWorker.Init(_base, newWaitingPoint);
                _base.AddWorkers(newWorker);
            }
        }
    }
}