using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class InputManagerCityBuilder : MonoBehaviour
    {
        public Action<Vector3Int> OnMouseClick, OnMouseHold;
        public Action OnMouseUp;
        
        private Vector2 _cameraMovementVector;
        public Vector2 cameraMovementVector => _cameraMovementVector;
        
        [SerializeField] private Camera mainCamera;
        public LayerMask groundMask;
        
        private Vector3 _lastPosition;

        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.nearClipPlane;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, groundMask))
            {
                _lastPosition = hit.point - new Vector3(0, 0, 0);
            }

            return _lastPosition;
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
            // EventSystem.current.IsPointerOverGameObject() checks for any UI elements over the cursor.
            if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var position = RaycastGround();
                if (position != null)
                    OnMouseHold?.Invoke(position.Value);
            }
        }

        private void CheckClickUpEvent()
        {
            // EventSystem.current.IsPointerOverGameObject() checks for any UI elements over the cursor.
            if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                OnMouseUp?.Invoke();
            }
        }

        private void CheckClickDownEvent()
        {
            // EventSystem.current.IsPointerOverGameObject() checks for any UI elements over the cursor.
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                var position = RaycastGround();
                if (position != null)
                    OnMouseClick?.Invoke(position.Value);
            }
        }
    }
}
