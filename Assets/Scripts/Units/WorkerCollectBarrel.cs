using System;
using BaseFunctions;
using Resource;
using UnityEngine;

namespace Units
{
    class WorkerCollectBarrel: MonoBehaviour
    {
        public void MoveToBarrel(Worker worker, Barrel barrel)
        {
            transform.position = Vector3.MoveTowards(transform.position, barrel.transform.position, worker.Speed * Time.deltaTime);
            transform.LookAt(barrel.transform);
        }

        public void BringBarrelToStock(Worker worker, Storage storage)
        {
            transform.position = Vector3.MoveTowards(transform.position, storage.transform.position, worker.Speed * Time.deltaTime);
            transform.LookAt(storage.transform);
        }

        public void MoveToWaitingPoint(Worker worker)
        {
            transform.position = Vector3.MoveTowards(transform.position, worker.WaitingPoint, worker.Speed * Time.deltaTime);
            transform.LookAt(worker.WaitingPoint);
        }

        public void PickUp(Worker worker, Barrel barrel)
        {
            barrel.transform.position = worker.transform.position + new Vector3(0,0.6f,0.5f);
            barrel.transform.parent = worker.transform;
            barrel.GetComponent<Collider>().enabled = false;
        }

        public void Drop(Barrel barrel, Vector3 storagePlace)
        {
            barrel.transform.parent = null;
            barrel.transform.position = storagePlace;
           
        }
    }
}