using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Benjamin_Test.City_Builder_Test.Scripts
{
    public class InputManager : MonoBehaviour
    {
        public Action<Vector3Int> OnMouseClick, OnMouseHold;
        public Action OnMouseUp;
        private Vector2 _cameraMovementVector;
        
        [SerializeField] private Camera mainCamera;
        public LayerMask groundMask;
        
        public Vector2 CameraMovementVector
        {
            get { return _cameraMovementVector; }
        }

        private void Update()
        {
            CheckClickDownEvent();
            CheckClickUpEvent();
            CheckClickHoldEvent();
            CheckArrowInput();
        }

        private Vector3Int? RaycastGround()
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
                return positionInt;
            }
            return null;
        }

        private void CheckArrowInput()
        {
            _cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private void CheckClickHoldEvent()
        {
            if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var position = RaycastGround();
                if (position != null)
                    OnMouseHold?.Invoke(position.Value);
            }
        }

        private void CheckClickUpEvent()
        {
            if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                OnMouseUp?.Invoke();
            }
        }

        private void CheckClickDownEvent()
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var position = RaycastGround();
                if (position != null)
                    OnMouseClick?.Invoke(position.Value);
            }
        }
    }
}
