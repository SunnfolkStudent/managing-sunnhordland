using UnityEngine;
using User_Interface__UI_;

namespace Building
{
    public class BuildManager : MonoBehaviour
    {
        public PlayerInputManager playerInputManager;
        public UIController uiController;
        
        /*private void Start()
        {
            uiController.PlacingRoad += RoadPlacementHandler;
            uiController.PlacingHouse += HousePlacementHandler;
            uiController.PlacingSpecial += NaturePlacementHandler;
            uiController.ExitBuildMode += ExitBuildModeHandler;
        }*/

        /*#region --- Currently Active Build State ---

        private void ExitBuildModeHandler()
        {
            ClearInputActions();
            Debug.Log("Leaving Build Mode.");
        }
        
        private void NaturePlacementHandler()
        {
            ClearInputActions();
            Debug.Log("Ready to place Nature.");
            playerInputManager.OnMouseClick += PlaceNature;
        }

        private void HousePlacementHandler()
        {
            ClearInputActions();
            Debug.Log("Ready to place Houses.");
            playerInputManager.OnMouseClick += PlaceHouse;
        }

        private void RoadPlacementHandler()
        {
            ClearInputActions();
            
            Debug.Log("Ready to place Roads.");
            playerInputManager.OnMouseClick += PlaceRoad;
            playerInputManager.OnMouseHold += PlaceRoad;
            playerInputManager.OnMouseUp += FinishPlacingRoad;
        }

        private void ClearInputActions()
        {
            playerInputManager.OnMouseClick = null;
            playerInputManager.OnMouseHold = null;
            playerInputManager.OnMouseUp = null;
        }

        #endregion*/
        
        private void PlaceNature(Vector2Int position)
        {
            // TODO: Add the ability to place prefabs.
        }
        
        private void PlaceHouse(Vector2Int position)
        {
            // TODO: Add the ability to place prefabs.
        }
        
        private void PlaceRoad(Vector2Int position)
        {
            // TODO: Add the ability to place prefabs.
        }

        private void FinishPlacingRoad()
        {
            // TODO: Add the ability to place prefabs.
        }
        
    }
}
