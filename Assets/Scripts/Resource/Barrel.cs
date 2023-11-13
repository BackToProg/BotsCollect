using UnityEngine;

namespace Resource
{
    public enum BarrelState
    {
        Idle,
        InAction
    }
    
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
