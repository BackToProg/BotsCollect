using System;
using System.Collections.Generic;
using Environment;
using Infrastructure.WorkerStateMachine;
using Units;
using UnityEngine;

namespace Outpost
{
    public class Base: MonoBehaviour
    {
        [SerializeField] private int _WorkerCount;
        [SerializeField] private Storage _storage;
        [SerializeField] private BarrelField _barrelField;
        
        private List<Worker> _workers;
        private Scanner _scanner;
        private WorkerFsmState _idleState;
        
        public Storage Storage => _storage;
        public int WorkersCount => _WorkerCount;
        public BarrelField BarrelField => _barrelField;
        
        public void AddWorkers(Worker worker)
        {
            _workers.Add(worker);
        }
        
        public void SendWorkerToCollectBarrel(Barrel barrel)
        {
            foreach (Worker worker in _workers)
            {
                if (worker.IsIdle)
                {
                    worker.GetTarget(barrel);
                    barrel.SetInActionState();
                    break;
                }
            }
        }

        private void Awake()
        {
            _workers = new List<Worker>();
        }
    }
}