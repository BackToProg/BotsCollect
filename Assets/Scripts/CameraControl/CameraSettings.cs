using UnityEngine;

namespace CameraControl
{
    public class CameraSettings : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _zoomSpeed = 10;
        
        [SerializeField] private KeyCode _forwardKey = KeyCode.W;
        [SerializeField] private KeyCode _backKey = KeyCode.S;
        [SerializeField] private KeyCode _leftKey = KeyCode.A;
        [SerializeField] private KeyCode _rightKey = KeyCode.D;
        [SerializeField] private KeyCode _upKey = KeyCode.E;
        [SerializeField] private KeyCode _downtKey = KeyCode.Q;
        
        [SerializeField] private KeyCode anchoredMoveKey = KeyCode.Mouse2;
        [SerializeField] private KeyCode anchoredRotateKey = KeyCode.Mouse1;


        public float MoveSpeed => _moveSpeed;

        public float RotationSpeed => _rotationSpeed;

        public float ZoomSpeed => _zoomSpeed;

        public KeyCode ForwardKey => _forwardKey;

        public KeyCode BackKey => _backKey;

        public KeyCode LeftKey => _leftKey;

        public KeyCode RightKey => _rightKey;

        public KeyCode UpKey => _upKey;

        public KeyCode DownKey => _downtKey;

        public KeyCode AnchoredMoveKey => anchoredMoveKey;

        public KeyCode AnchoredRotateKey => anchoredRotateKey;
    }
}
