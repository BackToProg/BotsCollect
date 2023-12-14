using Environment;
using Units;
using UnityEngine;
using Utils;

namespace Outpost
{
    public class BaseCreator : MonoBehaviour
    {
        [SerializeField] private Base _template;
        [SerializeField] private float _positionY = 0.2f;

        public void CreateBase(Vector3 basePosition, Worker worker, Base firstBase)
        {
            Base newBase = Instantiate(_template, basePosition, Quaternion.Euler(0, 180, 0));
            newBase.Init(firstBase.BarrelField, 0);

            Vector3 newSpawnPoint = SupportFunctions.DefinePointInArea(newBase.WorkerSpawner.SpawnPointCenter,
                newBase.WorkerSpawner.Radius, _positionY);
            WaitingPoint newWaitingPoint = Instantiate(newBase.WorkerSpawner.WaitingPointTemplate, newSpawnPoint,
                Quaternion.identity);
            worker.Init(newBase, newWaitingPoint);
            worker.GetTargetToMove(newWaitingPoint.transform);
            worker.WorkerFiniteStateMachine.Update();
        }
    }
}