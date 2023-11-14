using System;
using System.Collections;
using BaseFunctions;
using Resource;
using UnityEngine;
using Utils;

namespace Units
{
    [RequireComponent(typeof(WorkerCollectBarrel))]
    [RequireComponent(typeof(WorkerAnimator))]
    public class WorkerMovement: MonoBehaviour
    {
        private Worker _worker;
        private Coroutine _activeCoroutine;
        private WorkerAnimator _animator;
        private WorkerCollectBarrel _workerCollectBarrel;
        private Barrel _targetBarrel;

        public void Init(Worker worker)
        {
            _worker = worker;
            _animator = GetComponent<WorkerAnimator>();
            _workerCollectBarrel = GetComponent<WorkerCollectBarrel>();
        }
        
        private void Update()
        {
            switch (_worker.State)
            {
                case WorkerState.MoveToBarrel:
                    _animator.RunWalkAnimation(_worker.Speed);
                    _activeCoroutine = StartCoroutine(MoveToBarrel());
                    break;
                
                case WorkerState.MoveToStock:
                    _animator.RunCarryAnimation(true);
                    StopCoroutine(_activeCoroutine);
                    _activeCoroutine = StartCoroutine(MoveToStock());
                    _worker.Base.Storage.IncreaseBarrelCount();
                    break;
                
                case WorkerState.MoveToWaitingPoint:
                    _animator.RunCarryAnimation(false);
                    StopCoroutine(_activeCoroutine);
                    _activeCoroutine = StartCoroutine(MoveToWaitingPoint());
                    break;
                
                case WorkerState.Idle:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Vector3.Distance(transform.position, _worker.WaitingPoint) < SupportFunction.Epsilon && _worker.State == WorkerState.MoveToWaitingPoint)
            {
                float stopSpeed = 0;
                StopCoroutine(_activeCoroutine);
                _animator.RunWalkAnimation(stopSpeed);
                _worker.ChangeState(WorkerState.Idle);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Barrel barrel))
            {
                _workerCollectBarrel.PickUp(_worker, _worker.TargetBarrel);
                _worker.ChangeState(WorkerState.MoveToStock);
            }

            if (collision.TryGetComponent(out Storage storage))
            {
                Vector3 storagePlace = _worker.Base.Storage.GetPlaceToStore();
                _workerCollectBarrel.Drop(_worker.TargetBarrel, storagePlace);
                _worker.ChangeState(WorkerState.MoveToWaitingPoint);
            }
        }

        private IEnumerator MoveToBarrel()
        {
            while (_worker.State == WorkerState.MoveToBarrel)
            {
                _workerCollectBarrel.MoveToBarrel(_worker, _worker.TargetBarrel);
                
                yield return null;
            }
        }
        
        private IEnumerator MoveToStock()
        {
            while (_worker.State == WorkerState.MoveToStock)
            {
                _workerCollectBarrel.BringBarrelToStock(_worker, _worker.Base.Storage);
                
                yield return null;
            }
        }
        
        private IEnumerator MoveToWaitingPoint()
        {
            while (_worker.State == WorkerState.MoveToWaitingPoint)
            {
                _workerCollectBarrel.MoveToWaitingPoint(_worker);
                
                yield return null;
            }
        } 
    }
}