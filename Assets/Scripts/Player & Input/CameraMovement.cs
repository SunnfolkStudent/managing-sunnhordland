using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player___Input
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private new Camera camera;

        private Vector3 _dragOrigin;

        private void Update()
        {
            PanCamera();
        }

        private void PanCamera()
        {
            // Save position of mouse in worldSpace when drag starts (first time clicked)

            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = _dragOrigin - camera.ScreenToWorldPoint(Input.mousePosition);
                print("origin" + _dragOrigin + "newPosition " + camera.ScreenToWorldPoint(Input.mousePosition) + "= difference" + difference);
                camera.transform.position += difference;
            }
        }
    }
}
