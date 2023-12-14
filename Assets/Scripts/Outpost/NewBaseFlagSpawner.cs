using System;
using MouseControl;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Outpost
{
    public class NewBaseFlagSpawner : MonoBehaviour
    {
        [SerializeField] private NewBaseFlag _template;
        
        private NewBaseFlag _newBaseFlag; 
        private bool _isFlagAlreadySet;
        
        public static NewBaseFlagSpawner Instance { get; private set; }
        
        public event EventHandler<NewBaseFlag> OnNewBaseFlagSet;
        public event EventHandler<NewBaseFlag> OnNewBaseFlagChange;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Существует более чем один экземпляр класса NewBaseFlagSpawner {transform} - {Instance}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        
        private void Update()
        {
            Vector3 flagPosition = MouseWorld.GetPosition();

            if (!Input.GetMouseButtonDown(0)) return;
            
            if (_isFlagAlreadySet)
            {
                _newBaseFlag.transform.position = flagPosition;
                OnNewBaseFlagChange?.Invoke(this, _newBaseFlag);
            }
            else
            {
                TryToSetFlag(flagPosition);
            }
        }

        public void SetStatusToInactive(bool isActive)
        {
            _isFlagAlreadySet = isActive;
        }

        private void TryToSetFlag(Vector3 flagPosition)
        {
            if (flagPosition == Vector3.zero) return;
            
            _newBaseFlag = Instantiate(_template, flagPosition, Quaternion.identity);

            if (_newBaseFlag == null) return;
            
            _newBaseFlag.Init(this);
            OnNewBaseFlagSet?.Invoke(this, _newBaseFlag);
            _isFlagAlreadySet = true;
        }
    }
}