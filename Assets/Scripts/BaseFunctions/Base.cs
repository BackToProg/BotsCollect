using System.Collections.Generic;
using Resource;
using Units;
using UnityEngine;

namespace BaseFunctions
{
    [RequireComponent(typeof(BaseScanner))]
    [RequireComponent(typeof(WorkerCollectBarrel))]
    public class Base: MonoBehaviour
    {
        [SerializeField] private int _SoldierCount;
        [SerializeField] private BarrelField _barrelField;
        [SerializeField] private Storage _storage;
        
        private List<Worker> _soldiers;
        private BaseScanner _baseScanner;
        private WorkerCollectBarrel _solderCollectBarrel;

        public List<Worker> Soldiers => _soldiers;
        public Storage Storage => _storage;
        public int SoldiersCount => _SoldierCount;

        private void Awake()
        {
            _soldiers = new List<Worker>();
            _baseScanner = GetComponent<BaseScanner>();
            _solderCollectBarrel = GetComponent<WorkerCollectBarrel>();
        }

        private void Update()
        {
            Barrel barrelToCollect = _baseScanner.ScanArea(_barrelField.Barrels);
            Worker workerToWork = SoldierToWork();


            if (barrelToCollect != null && workerToWork != null)
            {
                barrelToCollect.ChangeState(BarrelState.InAction);
                workerToWork.GetTarget(barrelToCollect);
                workerToWork.ChangeState(SoldierState.MoveToBarrel);
            }
        }

        private Worker SoldierToWork()
        {
            foreach (var soldier in _soldiers)
            {
                if (soldier.SoldierState == SoldierState.Idle)
                {
                    return soldier;
                }
            }

            return null;
        }
        
        public void AddSoldiers(Worker worker)
        {
            _soldiers.Add(worker);
        }
    }
}