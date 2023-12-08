using Environment;
using Infrastructure.WorkerStateMachine;
using Outpost;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(WorkerAnimator))]
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
            _workerFsm = new WorkerFsm();
            _animator = GetComponent<WorkerAnimator>();
            WorkerStateMachineInit();
        }

        public void GetTarget(Barrel barrel)
        {
            _targetBarrel = barrel;
            _workerFsm.UpdateMoveTarget(barrel.transform);
            _animator.RunWalkAnimation(_speed);
        }

        public void ClearTargetBarrel()
        {
            _targetBarrel = null;
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