using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.WorkerStateMachine
{
    public class WorkerFsm
    {
        private WorkerFsmState _currentState;
        private Transform _targetToMove;

        private readonly Dictionary<Type, WorkerFsmState> _states = new Dictionary<Type, WorkerFsmState>();

        public Transform TargetToMove => _targetToMove;
        public WorkerFsmState CurrentState => _currentState;

        public void UpdateMoveTarget(Transform target)
        {
            _targetToMove = target;
        }

        public void AddState(WorkerFsmState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : WorkerFsmState
        {
            var type = typeof(T);

            if (_currentState != null && _currentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}