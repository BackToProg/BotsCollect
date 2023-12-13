using Units;
using UnityEngine;

namespace Outpost
{
    public class NewWorkerCreator : MonoBehaviour
    {
        [SerializeField] private WorkerSpawner _workerSpawner;

        public void CreateNewWorker()
        {
            _workerSpawner.SpawnNewWorker();
        }
    }
}