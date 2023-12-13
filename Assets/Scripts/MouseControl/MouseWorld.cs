using System;
using UnityEngine;

namespace MouseControl
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _mousePlaneLayerMask;

        private static MouseWorld _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static Vector3 GetPosition()
        {
            Ray cameraRay = _instance._camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(cameraRay, out RaycastHit raycastHit,float.MaxValue, _instance._mousePlaneLayerMask);

            return raycastHit.point;
        }
    }
}