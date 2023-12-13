using Environment;
using Outpost;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Worker))]
    [RequireComponent(typeof(BarrelInteraction))]
    [RequireComponent(typeof(BaseCreator))]
    public class WorkerMover : MonoBehaviour
    {
        private Worker _worker;
        private BarrelInteraction _barrelInteraction;
        private BaseCreator _baseCreator;

        private void Awake()
        {
            _worker = GetComponent<Worker>();
            _barrelInteraction = GetComponent<BarrelInteraction>();
            _baseCreator = GetComponent<BaseCreator>();
        }

        private void Update()
        {
            if (_worker.WorkerFsm.TargetToMove != null)
            {
                _worker.WorkerFsm.Update();
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Barrel barrel))
            {
                if (barrel == _worker.TargetBarrel)
                {
                    PickUpBarrel(barrel);
                }
            }

            if (collision.TryGetComponent(out Storage storage))
            {
                DropBarrelToStock(storage);
            }

            if (collision.TryGetComponent(out WaitingPoint waitingPoint) &&
                _worker.IsIdle == false
                && waitingPoint == _worker.WaitingPoint)
            {
                ReachedWaitingPoint();
            }

            if (collision.TryGetComponent(out NewBaseFlag newBaseFlag))
            {
                Destroy(newBaseFlag.gameObject);
                _baseCreator.CreateBase(newBaseFlag.transform.position, _worker, _worker.Base);
            }
        }

        private void ReachedWaitingPoint()
        {
            float stopSpeed = 0f;

            _worker.WorkerFsm.UpdateMoveTarget(null);
            _worker.Animator.RunWalkAnimation(stopSpeed);
            _worker.WorkerFsm.Update();
        }

        private void PickUpBarrel(Barrel barrel)
        {
            _worker.WorkerFsm.UpdateMoveTarget(_worker.Base.Storage.transform);
            _barrelInteraction.PickUp(_worker, barrel);
            _worker.Base.BarrelField.RemoveBarrelFromField();
            _worker.Animator.RunCarryAnimation(true);
        }

        private void DropBarrelToStock(Storage storage)
        {
            Vector3 storagePlace = storage.GetPlaceToStore();

            _worker.WorkerFsm.UpdateMoveTarget(_worker.WaitingPoint.transform);
            storage.AddBarrel(_worker.TargetBarrel);
            _barrelInteraction.Drop(_worker.TargetBarrel, storagePlace);
            _worker.ClearTargetBarrel();
            _worker.Animator.RunCarryAnimation(false);
        }
    }
}