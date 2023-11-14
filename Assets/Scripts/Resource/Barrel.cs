using UnityEngine;
using Utils;

namespace Resource
{
    public class Barrel : MonoBehaviour
    {
        public BarrelState BarrelState { get; private set; }

        public void Init()
        {
            BarrelState = BarrelState.Idle;
        }

        public void ChangeState(BarrelState barrelState)
        {
            BarrelState = barrelState;
        }
    }
}
