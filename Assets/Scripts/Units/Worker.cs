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
        private WorkerFiniteStateMachine _workerFiniteStateMachine;
        private WorkerFiniteStateMachineStateIdle _stateMachineStateIdle;
        private WorkerFiniteStateMachineStateMove _stateMachineStateMove;
        private Barrel _targetBarrel;
        private WorkerAnimator _animator;

        public Barrel TargetBarrel => _targetBarrel;
        public WorkerFiniteStateMachine WorkerFiniteStateMachine => _workerFiniteStateMachine;
        public WaitingPoint WaitingPoint => _waitingPoint;
        public Base Base => _base;
        public WorkerAnimator Animator => _animator;

        public bool IsIdle => _workerFiniteStateMachine.CurrentStateMachineState == _stateMachineStateIdle;
        
        private void Awake()
        {
            _animator = GetComponent<WorkerAnimator>();
            _workerFiniteStateMachine = new WorkerFiniteStateMachine();
            WorkerStateMachineInit();
        }

        public void Init(Base outpost, WaitingPoint waitingPoint)
        {
            _base = outpost;
            _waitingPoint = waitingPoint;
        }

        public void SetTargetBarrel(Barrel barrel)
        {
            _targetBarrel = barrel;
            GetTargetToMove(barrel.transform);

        }

        public void GetTargetToMove(Transform target)
        {
            _workerFiniteStateMachine.UpdateMoveTarget(target);
            _animator.RunWalkAnimation(_speed);
        }

        public void ClearTargetBarrel()
        {
            _targetBarrel = null;
        }
        

        private void WorkerStateMachineInit()
        {
            _stateMachineStateIdle = new WorkerFiniteStateMachineStateIdle(_workerFiniteStateMachine);
            _stateMachineStateMove = new WorkerFiniteStateMachineStateMove(_workerFiniteStateMachine, transform, _speed);

            _workerFiniteStateMachine.AddState(_stateMachineStateIdle);
            _workerFiniteStateMachine.AddState(_stateMachineStateMove);
            _workerFiniteStateMachine.SetState<WorkerFiniteStateMachineStateIdle>();
        }
    }
}