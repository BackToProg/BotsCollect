using System.Collections;
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
        [SerializeField] private int _barrelCountForNewWorker = 3;
        [SerializeField] private int _barrelCountForNewBase = 5;

        private List<Worker> _workers;
        private Scanner _scanner;
        private WorkerFiniteStateMachineState _idleStateMachineState;
        private NewWorkerCreator _workerCreator;
        private bool _isNewBaseFlagSet;
        private Transform _newBaseTransform;
        private Coroutine _createNewWorkerCoroutine;
        private Coroutine _createNewBaseCoroutine;
        

        public Storage Storage => _storage;
        public int WorkersInitialCount => _workerInitialCount;
        public BarrelField BarrelField => _barrelField;
        public WorkerSpawner WorkerSpawner => _workerSpawner;

        private void Awake()
        {
            _workers = new List<Worker>();
            _workerCreator = GetComponent<NewWorkerCreator>();
        }

        private void Start()
        {
            NewBaseFlagSpawner.Instance.OnNewBaseFlagSet += OnNewBaseFlagSet;
            NewBaseFlagSpawner.Instance.OnNewBaseFlagChange += OnNewBaseFlagChange;
        }

        private void Update()
        {
            _createNewWorkerCoroutine = StartCoroutine(CreateNewWorker());
            _createNewBaseCoroutine = StartCoroutine(CreateNewBase());
        }

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
                barrel.SetInActionState();
                worker.SetTargetBarrel(barrel);
            }
        }
        
        private IEnumerator CreateNewWorker()
        {
            while (_isNewBaseFlagSet == false)
            {
                if (_storage.BarrelCount >= _barrelCountForNewWorker)
                {
                    StopCoroutineWithActivityCheck(_createNewBaseCoroutine);
                    _workerCreator.CreateNewWorker();
                    _storage.IssueBarrels(_barrelCountForNewWorker);
                }
                
                yield return null;
            }
        }
        
        private IEnumerator CreateNewBase()
        {
            while (_isNewBaseFlagSet)
            {
                Worker worker = TryGetWorkerInIdleAvailable();

                if (worker != null && _storage.BarrelCount >= _barrelCountForNewBase)
                {
                    StopCoroutineWithActivityCheck(_createNewWorkerCoroutine);
                    SendWorkerToBuiltNewBase(worker);
                    _storage.IssueBarrels(_barrelCountForNewBase);
                    _isNewBaseFlagSet = false;
                }
                
                yield return null;
            }
        }

        private void StopCoroutineWithActivityCheck(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
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

        private void OnNewBaseFlagChange(object sender, NewBaseFlag newBaseFlag)
        {
            _newBaseTransform = newBaseFlag.transform;
        }

        private void OnNewBaseFlagSet(object sender, NewBaseFlag newBaseFlag)
        {
            _isNewBaseFlagSet = true;
            _newBaseTransform = newBaseFlag.transform;
        }
    }
}