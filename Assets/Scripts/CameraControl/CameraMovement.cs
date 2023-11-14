using UnityEngine;

namespace CameraControl
{
    [RequireComponent(typeof(CameraSettings))]
    public class CameraMovement: MonoBehaviour
    {
        private CameraSettings _camera;

        private void Awake()
        {
            _camera = GetComponent<CameraSettings>();
        }

        private void FixedUpdate()
        {
            Vector3 move = Vector3.zero;

            if (Input.GetKey(_camera.ForwardKey))
                move += Vector3.forward * (_camera.MoveSpeed * Time.deltaTime);
            if (Input.GetKey(_camera.BackKey))
                move += Vector3.back * (_camera.MoveSpeed * Time.deltaTime);
            if (Input.GetKey(_camera.LeftKey))
                move += Vector3.left * (_camera.MoveSpeed * Time.deltaTime);
            if (Input.GetKey(_camera.RightKey))
                move += Vector3.right * (_camera.MoveSpeed * Time.deltaTime);
            if (Input.GetKey(_camera.UpKey))
                move += Vector3.up * (_camera.MoveSpeed * Time.deltaTime);
            if (Input.GetKey(_camera.DownKey))
                move += Vector3.down * (_camera.MoveSpeed * Time.deltaTime);
            
            float mouseMoveY = Input.GetAxis("Mouse Y");
            float mouseMoveX = Input.GetAxis("Mouse X");
            
            if (Input.GetKey(_camera.AnchoredMoveKey))
            {
                move += Vector3.up * (mouseMoveY * -_camera.MoveSpeed * Time.deltaTime);
                move += Vector3.right * (mouseMoveY * -_camera.MoveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(_camera.AnchoredRotateKey))
            {
                transform.RotateAround(transform.position, transform.right, mouseMoveY * -_camera.RotationSpeed * Time.deltaTime );
                transform.RotateAround(transform.position, Vector3.up, mouseMoveX * _camera.RotationSpeed * Time.deltaTime );
            }
            
            transform.Translate(move);
        }

        private void LateUpdate()
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(Vector3.forward * (mouseScroll * _camera.ZoomSpeed * Time.deltaTime));
        }
    }
}