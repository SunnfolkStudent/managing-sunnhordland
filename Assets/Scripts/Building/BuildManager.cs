using UnityEngine;
using User_Interface__UI_;

namespace Building
{
    public class BuildManager : MonoBehaviour
    {
        public static bool InBuildMode;
        
        public PlayerInputManager playerInputManager;
        public UIController uiController;
        
        private void Start()
        {
            uiController.EnteringBuildMode += EnterBuildModeHandler;
            uiController.ExitBuildMode += ExitBuildModeHandler;
        }

        #region --- Currently Active Build State ---

        private void ExitBuildModeHandler()
        {
            ClearInputActions();
            InBuildMode = false;
            Debug.Log("Leaving Build Mode.");
        }
        
        private void EnterBuildModeHandler(int gameObjectToBuild)
        {
            ClearInputActions();
            InBuildMode = true;
            Debug.Log("Ready to place buildings.");
            playerInputManager.OnMouseClick += PlaceStructure;
        }

        /*private void RoadPlacementHandler()
        {
            ClearInputActions();
            
            Debug.Log("Ready to place Roads.");
            playerInputManager.OnMouseClick += PlaceRoad;
            playerInputManager.OnMouseHold += PlaceRoad;
            playerInputManager.OnMouseUp += FinishPlacingRoad;
        }*/

        // Used to clear stored inputs between phases / events.
        private void ClearInputActions()
        {
            playerInputManager.OnMouseClick = null;
            playerInputManager.OnMouseHold = null;
            playerInputManager.OnMouseUp = null;
        }

        #endregion
        
        private void PlaceStructure(Vector2Int position)
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
