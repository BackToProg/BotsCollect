using UnityEngine;

namespace Environment
{
    public class Barrel: MonoBehaviour
    {
        private bool _isInAction;

        public bool IsInAction => _isInAction;

        public void SetInActionState() => _isInAction = true;

    }
}