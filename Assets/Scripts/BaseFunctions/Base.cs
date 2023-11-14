using System.Collections.Generic;
using Resource;
using Units;
using UnityEngine;
using Utils;

namespace BaseFunctions
{
    [RequireComponent(typeof(BaseScanner))]
    [RequireComponent(typeof(WorkerCollectBarrel))]
    public class Base: MonoBehaviour
    {
        [SerializeField] private int _SoldierCount;
        [SerializeField] private Storage _storage;
        
        private List<Worker> _workers;
        private BaseScanner _baseScanner;
        
        public Storage Storage => _storage;
        public int SoldiersCount => _SoldierCount;
        
        public void AddWorkers(Worker worker)
        {
            _workers.Add(worker);
        }

        private void Awake()
        {
            _workers = new List<Worker>();
            _baseScanner = GetComponent<BaseScanner>();
        }

        private void Update()
        {
            Barrel barrelToCollect = _baseScanner.ScanArea();
            Worker workerToWork = WorkerToWork();
            
            if (barrelToCollect != null && workerToWork != null)
            {
                barrelToCollect.ChangeState(BarrelState.InAction);
                workerToWork.GetTarget(barrelToCollect);
                workerToWork.ChangeState(WorkerState.MoveToBarrel);
            }
        }

        private Worker WorkerToWork()
        {
            foreach (var soldier in _workers)
            {
                if (soldier.State == WorkerState.Idle)
                {
                    return soldier;
                }
            }

            return null;
        }
    }
}