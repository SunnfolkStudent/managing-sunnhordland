using UnityEngine;

namespace Player___Input
{
    public class InputManager2 : MonoBehaviour
    {
        [SerializeField] private Camera sceneCamera;

        private Vector3 _lastPosition;

        [SerializeField]
        private float testZDistanceFromPlane;

        [SerializeField] private LayerMask placementLayerMask;

        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
            {
                _lastPosition = hit.point - new Vector3(0, 0, testZDistanceFromPlane);
            }

            return _lastPosition;
        }
    }
}
