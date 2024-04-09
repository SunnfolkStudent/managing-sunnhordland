using Benjamin_Test.City_Builder_Test.Scripts;
using SVS;
using UnityEngine;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public CameraMovement cameraMovement;
        public RoadManager roadManager;
        public InputManager inputManager;

        private void Start()
        {
            inputManager.OnMouseClick += roadManager.PlaceRoad;
            inputManager.OnMouseHold += roadManager.PlaceRoad;
            inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
        }

        private void HandleMouseClick(Vector3Int position)
        {
            Debug.Log(position);
            roadManager.PlaceRoad(position);
        }

        private void Update()
        {
            cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
        }
    }
}
