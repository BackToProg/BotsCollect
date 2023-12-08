using Environment;
using UnityEngine;

namespace Outpost
{
    [RequireComponent(typeof(Base))]
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private BarrelField _barrelField;

        private Base _base;

        private void Awake()
        {
            _base = GetComponent<Base>();
        }

        private void Start()
        {
            _barrelField.OnFreeBarrel += BarrelFieldOnFreeBarrel;
        }

        private void BarrelFieldOnFreeBarrel(object sender, Barrel barrel)
        {
            _base.SendWorkerToCollectBarrel(barrel);
        }
    }
}