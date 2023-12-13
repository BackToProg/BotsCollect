using System.Collections.Generic;
using Environment;
using Infrastructure.WorkerStateMachine;
using Units;
using UnityEngine;

namespace Outpost
{
    [RequireComponent(typeof(NewWorkerCreator))]
    public class Base : MonoBehaviour
    {
        [SerializeField] private int _workerInitialCount;
        [SerializeField] private Storage _storage;
        [SerializeField] private BarrelField _barrelField;
        [SerializeField] private WorkerSpawner _workerSpawner;

        private List<Worker> _workers;
        private Scanner _scanner;
        private WorkerFsmState _idleState;
        private NewWorkerCreator _workerCreator;
        private bool _isNewBaseFlagSet;
        private Transform _newBaseTransform;

        public Storage Storage => _storage;
        public int WorkersInitialCount => _workerInitialCount;
        public BarrelField BarrelField => _barrelField;
        public WorkerSpawner WorkerSpawner => _workerSpawner;

        public void Init(BarrelField barrelField, int workerCount)
        {
            _barrelField = barrelField;
            _workerInitialCount = workerCount;
        }

        public void AddWorkers(Worker worker)
        {
            _workers.Add(worker);
        }

        public void SendWorkerToCollectBarrel(Barrel barrel)
        {
            Worker worker = TryGetWorkerInIdleAvailable();

            if (worker)
            {
                worker.GetTargetBarrel(barrel);
                barrel.SetInActionState();
            }
        }

        private void SendWorkerToBuiltNewBase(Worker worker)
        {
            worker.GetTargetToMove(_newBaseTransform);
        }

        private Worker TryGetWorkerInIdleAvailable()
        {
            foreach (Worker worker in _workers)
            {
                if (worker.IsIdle)
                {
                    return worker;
                }
            }

            return null;
        }

        private void Awake()
        {
            _workers = new List<Worker>();
            _workerCreator = GetComponent<NewWorkerCreator>();
        }

        private void Start()
        {
            NewBaseFlagSpawner.Instance.OnNewBaseFlagSet += NewBaseFlagSpawner_OnNewBaseFlagSet;
            NewBaseFlagSpawner.Instance.OnNewBaseFlagChange += NewBaseFlagSpawner_OnNewBaseFlagChange;
        }

        private void NewBaseFlagSpawner_OnNewBaseFlagChange(object sender, NewBaseFlag newBaseFlag)
        {
            _newBaseTransform = newBaseFlag.transform;
        }

        private void NewBaseFlagSpawner_OnNewBaseFlagSet(object sender, NewBaseFlag newBaseFlag)
        {
            _isNewBaseFlagSet = true;
            _newBaseTransform = newBaseFlag.transform;
        }

        private void Update()
        {
            int barrelCountForNewWorker = 3;
            int barrelCountForNewBase = 5;

            if (_isNewBaseFlagSet == false && _storage.BarrelCount >= barrelCountForNewWorker)
            {
                _workerCreator.CreateNewWorker();
                _storage.IssueBarrels();
            }

            if (_isNewBaseFlagSet && _storage.BarrelCount >= barrelCountForNewBase)
            {
                Worker worker = TryGetWorkerInIdleAvailable();

                if (worker != null)
                {
                    SendWorkerToBuiltNewBase(worker);
                    _storage.IssueBarrels();
                    _isNewBaseFlagSet = false;
                }
            }
        }
    }
}