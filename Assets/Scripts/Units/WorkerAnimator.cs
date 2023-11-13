using UnityEngine;

namespace Units
{
    public class WorkerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsCarry = Animator.StringToHash("IsCarry");

        public void RunWalkAnimation(float speed)
        {
            _animator.SetFloat(Speed,speed);
        }

        public void RunCarryAnimation(bool hasBarrel)
        {
            _animator.SetBool(IsCarry, hasBarrel);
        }
    }
}
