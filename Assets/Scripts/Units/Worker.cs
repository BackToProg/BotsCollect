using System;
using Environment;
using Infrastructure.WorkerStateMachine;
using Outpost;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Units
{
    [RequireComponent(typeof(WorkerAnimator))]
    [RequireComponent(typeof(BaseCreator))]
    public class Worker : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Base _base;
        private WaitingPoint _waitingPoint;
        private WorkerFsm _workerFsm;
        private WorkerFsmStateIdle _stateIdle;
        private WorkerFsmStateMove _stateMove;
        private Barrel _targetBarrel;
        private WorkerAnimator _animator;

        public Barrel TargetBarrel => _targetBarrel;
        public WorkerFsm WorkerFsm => _workerFsm;
        public WaitingPoint WaitingPoint => _waitingPoint;
        public Base Base => _base;
        public WorkerAnimator Animator => _animator;

        public bool IsIdle => _workerFsm.CurrentState == _stateIdle;

        public void Init(Base outpost, WaitingPoint waitingPoint)
        {
            _base = outpost;
            _waitingPoint = waitingPoint;
        }

        public void GetTargetBarrel(Barrel barrel)
        {
            _targetBarrel = barrel;
            GetTargetToMove(barrel.transform);

        }

        public void GetTargetToMove(Transform target)
        {
            _workerFsm.UpdateMoveTarget(target);
            _animator.RunWalkAnimation(_speed);
        }

        public void ClearTargetBarrel()
        {
            _targetBarrel = null;
        }

        private void Awake()
        {
            _animator = GetComponent<WorkerAnimator>();
            _workerFsm = new WorkerFsm();
            WorkerStateMachineInit();
        }

        private void WorkerStateMachineInit()
        {
            _stateIdle = new WorkerFsmStateIdle(_workerFsm);
            _stateMove = new WorkerFsmStateMove(_workerFsm, transform, _speed);

            _workerFsm.AddState(_stateIdle);
            _workerFsm.AddState(_stateMove);
            _workerFsm.SetState<WorkerFsmStateIdle>();
        }
    }
}