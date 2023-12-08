using Environment;
using UnityEngine;

namespace Units
{
    public class BarrelInteraction : MonoBehaviour
    {
        public void PickUp(Worker worker, Barrel barrel)
        {
            Vector3 barrelPickedUpPosition = new Vector3(0, 0.6f, 0.5f);

            barrel.transform.position = worker.transform.position + barrelPickedUpPosition;
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