using System;
using System.Collections;
using BaseFunctions;
using Resource;
using UnityEngine;
using Utils;


namespace Units
{
    [RequireComponent(typeof(WorkerMovement))]
    public class Worker : MonoBehaviour
    {
        [SerializeField] private float _speed;
       
        private Base _base;
        private Barrel _targetBarrel;
        private WorkerMovement _workerMovement;
        private Vector3 _waitingPoint;
        private WorkerState _state;
        
        public Barrel TargetBarrel => _targetBarrel;
        
        public Base Base => _base;
        
        public WorkerState State => _state;
        
        public Vector3 WaitingPoint => _waitingPoint;
        
        public float Speed => _speed;

        public void Init(Base soldiersBase, Vector3 waitingPoint)
        {
            _base = soldiersBase;
            _waitingPoint = waitingPoint;
            _state = WorkerState.Idle;
            _workerMovement = GetComponent<WorkerMovement>();
            _workerMovement.Init(this);
        }

        public void GetTarget(Barrel barrel)
        {
            _targetBarrel = barrel;
        }
        
        public void ChangeState(WorkerState workerState)
        {
            _state = workerState;
        }

        
    }
}
