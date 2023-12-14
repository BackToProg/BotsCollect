using System;
using UnityEngine;

namespace Outpost
{
    public class NewBaseFlag : MonoBehaviour
    {
        private NewBaseFlagSpawner _newBaseFlagSpawner;

        private void OnDestroy()
        {
            _newBaseFlagSpawner.SetStatusToInactive(false);
        }

        public void Init(NewBaseFlagSpawner newBaseFlagSpawner)
        {
            _newBaseFlagSpawner = newBaseFlagSpawner; 
        }
    }
}