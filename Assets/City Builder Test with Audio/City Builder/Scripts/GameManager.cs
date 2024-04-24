using SVS;
using UnityEngine;
using UnityEngine.Serialization;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public CameraMovement cameraMovement;
        public RoadManager roadManager;
        [FormerlySerializedAs("inputManager")] public InputManagerCityBuilder inputManagerCityBuilder;

        public UIControllerSvs uiControllerSvs;

        public StructureManager structureManager;

        private void Start()
        {
            uiControllerSvs.OnRoadPlacement += RoadPlacementHandler;
            uiControllerSvs.OnHousePlacement += HousePlacementHandler;
            uiControllerSvs.OnSpecialPlacement += SpecialPlacementHandler;
            uiControllerSvs.OnBigStructurePlacement += BigStructurePlacementHandler;
        }

        private void BigStructurePlacementHandler()
        {
            ClearInputActions();
            inputManagerCityBuilder.OnMouseClick += structureManager.PlaceBigStructure;
        }

        private void SpecialPlacementHandler()
        {
            ClearInputActions();
            inputManagerCityBuilder.OnMouseClick += structureManager.PlaceSpecial;
        }

        private void HousePlacementHandler()
        {
            ClearInputActions();
            inputManagerCityBuilder.OnMouseClick += structureManager.PlaceHouse;
        }

        private void RoadPlacementHandler()
        {
            ClearInputActions();
            
            inputManagerCityBuilder.OnMouseClick += roadManager.PlaceRoad;
            inputManagerCityBuilder.OnMouseHold += roadManager.PlaceRoad;
            inputManagerCityBuilder.OnMouseUp += roadManager.FinishPlacingRoad;
        }

        private void ClearInputActions()
        {
            inputManagerCityBuilder.OnMouseClick = null;
            inputManagerCityBuilder.OnMouseHold = null;
            inputManagerCityBuilder.OnMouseUp = null;
        }

        private void Update()
        {
            cameraMovement.MoveCamera(new Vector3(inputManagerCityBuilder.CameraMovementVector.x, 0, inputManagerCityBuilder.CameraMovementVector.y));
        }
    }
}
