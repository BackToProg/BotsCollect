using Environment;
using UnityEngine;

namespace Outpost
{
    [RequireComponent(typeof(Base))]
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private BarrelField _barrelField;

        private Base _base;

        public void Init(BarrelField barrelField)
        {
            _barrelField = barrelField;
        }

        private void Awake()
        {
            _base = GetComponent<Base>();
        }

        private void Start()
        {
            if (_barrelField != null)
            {
                _barrelField.OnFreeBarrel += BarrelFieldOnFreeBarrel;
            }
        }

        private void BarrelFieldOnFreeBarrel(object sender, Barrel barrel)
        {
            _base.SendWorkerToCollectBarrel(barrel);
        }
    }
}