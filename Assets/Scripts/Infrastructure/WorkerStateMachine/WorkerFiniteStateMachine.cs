using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.WorkerStateMachine
{
    public class WorkerFiniteStateMachine
    {
        private WorkerFiniteStateMachineState _currentStateMachineState;
        private Transform _targetToMove;

        private readonly Dictionary<Type, WorkerFiniteStateMachineState> _states = new Dictionary<Type, WorkerFiniteStateMachineState>();

        public Transform TargetToMove => _targetToMove;
        public WorkerFiniteStateMachineState CurrentStateMachineState => _currentStateMachineState;

        public void UpdateMoveTarget(Transform target)
        {
            _targetToMove = target;
        }

        public void AddState(WorkerFiniteStateMachineState stateMachineState)
        {
            _states.Add(stateMachineState.GetType(), stateMachineState);
        }

        public void SetState<T>() where T : WorkerFiniteStateMachineState
        {
            var type = typeof(T);

            if (_currentStateMachineState != null && _currentStateMachineState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                _currentStateMachineState?.Exit();
                _currentStateMachineState = newState;
                _currentStateMachineState.Enter();
            }
        }

        public void Update()
        {
            _currentStateMachineState?.Update();
        }
    }
}