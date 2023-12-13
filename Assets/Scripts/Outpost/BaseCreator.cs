using Environment;
using Units;
using UnityEngine;
using Utils;

namespace Outpost
{
    public class BaseCreator : MonoBehaviour
    {
        [SerializeField] private Base _template;

        public void CreateBase(Vector3 basePosition, Worker worker, Base firstBase)
        {
            Debug.Log(basePosition);
            Base newBase = Instantiate(_template, basePosition, Quaternion.Euler(0, 180, 0));
            newBase.Init(firstBase.BarrelField, 0);
            Scanner scanner = newBase.GetComponent<Scanner>();
            scanner.Init(firstBase.BarrelField);

            Vector3 newSpawnPoint = SupportFunctions.DefinePointInArea(newBase.WorkerSpawner.SpawnPointArea,
                newBase.WorkerSpawner.SpawnRadius);
            WaitingPoint newWaitingPoint = Instantiate(newBase.WorkerSpawner.WaitingPointTemplate, newSpawnPoint,
                Quaternion.identity);
            worker.Init(newBase, newWaitingPoint);
            worker.GetTargetToMove(newWaitingPoint.transform);
            worker.WorkerFsm.Update();
        }
    }
}