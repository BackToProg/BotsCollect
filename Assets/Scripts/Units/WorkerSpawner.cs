using BaseFunctions;
using UnityEngine;
using Utils;

namespace Units
{
    
    public class WorkerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPointArea;
        [SerializeField] private Worker worker;
        [SerializeField] private int _spawnRadius;
        [SerializeField] private Base _base;

        private void Awake()
        {
            for (int i = 0; i < _base.SoldiersCount; i++)
            {
                Vector3 spawnPoint = SupportFunction.DefinePointInArea(_spawnPointArea, _spawnRadius);
                Worker newWorker = Instantiate(worker, spawnPoint, Quaternion.identity);
                newWorker.Init(_base, newWorker.transform.position);
                _base.AddWorkers(newWorker);
            }
        }
    }
}