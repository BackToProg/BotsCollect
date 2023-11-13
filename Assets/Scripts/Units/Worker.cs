using System;
using System.Collections;
using BaseFunctions;
using Resource;
using Units;
using UnityEngine;


namespace Units
{
    public enum SoldierState
    {
        Idle,
        MoveToBarrel,
        MoveToStock,
        MoveToWaitingPoint
    }
    
    [RequireComponent(typeof(WorkerCollectBarrel))]
    public class Worker : MonoBehaviour
    {
        [SerializeField] private float _speed;
       
        private Base _base;
        private Vector3 _waitingPoint;
        private SoldierState _soldierState;
        private WorkerCollectBarrel _workerCollectBarrel;
        private Vector3 _storagePlace;
        private Barrel _targetBarrel;
        private WorkerAnimator _animator;
        private Coroutine _activeCoroutine;

        public SoldierState SoldierState => _soldierState;
        public float Speed => _speed;
        public Vector3 WaitingPoint => _waitingPoint;

        public void Init(Base soldiersBase, Vector3 waitingPoint)
        {
            _base = soldiersBase;
            _waitingPoint = waitingPoint;
            _soldierState = SoldierState.Idle;
            _animator = GetComponent<WorkerAnimator>();
            _workerCollectBarrel = GetComponent<WorkerCollectBarrel>();
        }

        public void GetTarget(Barrel barrel)
        {
            _targetBarrel = barrel;
        }
        
        public void ChangeState(SoldierState soldierState)
        {
            _soldierState = soldierState;
        }

        private void Update()
        {
            switch (_soldierState)
            {
                case SoldierState.MoveToBarrel:
                    _animator.RunWalkAnimation(_speed);
                    _activeCoroutine = StartCoroutine(MoveToBarrel());
                    break;
                
                case SoldierState.MoveToStock:
                    _animator.RunCarryAnimation(true);
                    StopCoroutine(_activeCoroutine);
                    _activeCoroutine = StartCoroutine(MoveToStock());
                    break;
                
                case SoldierState.MoveToWaitingPoint:
                    _animator.RunCarryAnimation(false);
                    StopCoroutine(_activeCoroutine);
                    _activeCoroutine = StartCoroutine(MoveToWaitingPoint());
                    break;
                
                case SoldierState.Idle:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Vector3.Distance(transform.position, _waitingPoint) < 0.01f && _soldierState == SoldierState.MoveToWaitingPoint)
            {
                StopCoroutine(_activeCoroutine);
                _animator.RunWalkAnimation(0);
                _soldierState = SoldierState.Idle;
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Barrel barrel))
            {
                _workerCollectBarrel.PickUp(this, _targetBarrel);
                _soldierState = SoldierState.MoveToStock;
            }

            if (collision.TryGetComponent(out Storage storage))
            {
                _storagePlace = _base.Storage.GetPlaceToStore();
                _workerCollectBarrel.Drop(_targetBarrel, _storagePlace);
                _soldierState = SoldierState.MoveToWaitingPoint;
            }
        }

        private IEnumerator MoveToBarrel()
        {
            while (_soldierState == SoldierState.MoveToBarrel)
            {
                _workerCollectBarrel.MoveToBarrel(this, _targetBarrel);
                
                yield return null;
            }
        }
        
        private IEnumerator MoveToStock()
        {
            while (_soldierState == SoldierState.MoveToStock)
            {
                _workerCollectBarrel.BringBarrelToStock(this, _base.Storage);
                
                yield return null;
            }
        }
        
        private IEnumerator MoveToWaitingPoint()
        {
            while (_soldierState == SoldierState.MoveToWaitingPoint)
            {
                _workerCollectBarrel.MoveToWaitingPoint(this);
                
                yield return null;
            }
        } 
    }
}
