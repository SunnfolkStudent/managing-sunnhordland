using Benjamin_Test.City_Builder_Test.Scripts;
using Player___Input;
using SVS;
using UnityEngine;
using UnityEngine.Serialization;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public CameraMovement cameraMovement;
        public RoadManager roadManager;
        public InputManager inputManager;

        public UIController uiController;

        public StructureManager structureManager;

        private void Start()
        {
            uiController.OnRoadPlacement += RoadPlacementHandler;
            uiController.OnHousePlacement += HousePlacementHandler;
            uiController.OnSpecialPlacement += SpecialPlacementHandler;
            uiController.OnBigStructurePlacement += BigStructurePlacementHandler;
        }

        private void BigStructurePlacementHandler()
        {
            ClearInputActions();
            inputManager.OnMouseClick += structureManager.PlaceBigStructure;
        }

        private void SpecialPlacementHandler()
        {
            ClearInputActions();
            inputManager.OnMouseClick += structureManager.PlaceSpecial;
        }

        private void HousePlacementHandler()
        {
            ClearInputActions();
            inputManager.OnMouseClick += structureManager.PlaceHouse;
        }

        private void RoadPlacementHandler()
        {
            ClearInputActions();
            
            inputManager.OnMouseClick += roadManager.PlaceRoad;
            inputManager.OnMouseHold += roadManager.PlaceRoad;
            inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
        }

        private void ClearInputActions()
        {
            inputManager.OnMouseClick = null;
            inputManager.OnMouseHold = null;
            inputManager.OnMouseUp = null;
        }

        private void Update()
        {
            cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
        }
    }
}
