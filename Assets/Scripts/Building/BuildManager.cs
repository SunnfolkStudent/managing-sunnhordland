using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using User_Interface__UI_;
using PlayerInputManager = Player___Input.PlayerInputManager;

namespace Building
{
    public class BuildManager : MonoBehaviour
    {
        private PurchasableItem[] _purchasableItems;
        
        [SerializeField] private List<BuildableObjectScrub> buildingScrubs;
        [SerializeField] private List<BuildableObjectScrub> roadScrubs;
        [SerializeField] private List<BuildableObjectScrub> natureScrubs;
        
        public static bool InBuildMode;
        
        public PlayerInputManager playerInputManager;
        public UIController uiController;
        public GridManager gridManager;

        private ExitButtonScript _exitShopButtonScript;
        
        private void Start()
        {
            _purchasableItems = FindObjectsByType<PurchasableItem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _exitShopButtonScript = FindFirstObjectByType<ExitButtonScript>(FindObjectsInactive.Include);
            Debug.Log(_exitShopButtonScript.gameObject);
            
            foreach (var item in _purchasableItems)
            {
                var itemType = item.itemScrub.itemType;
                item.EnteringBuildMode += EnterBuildModeHandler;
                
                switch (itemType)
                {
                    case TileType.Building:
                        buildingScrubs.Add(item.itemScrub);
                        continue;
                    case TileType.Nature:
                        natureScrubs.Add(item.itemScrub);
                        continue;
                    case TileType.Road:
                        roadScrubs.Add(item.itemScrub);
                        continue;
                    case TileType.Empty:
                        Debug.LogWarning("Whoops..." + item.itemScrub + " is TileType.Empty!");
                        continue;
                    default:
                        Debug.LogWarning("Whoops..." + item.itemScrub + " is not assigned a TileType!");
                        continue;
                }
            }
            
            
            
            uiController.EnteringBuildMode += EnterBuildModeHandler;
            uiController.ExitBuildMode += ExitBuildModeHandler;
        }

        // Remove after properly implementing mouse on UI
        private void Update()
        {
            if (Keyboard.current.bKey.wasPressedThisFrame && InBuildMode)
            {
                ExitBuildModeHandler();
            }
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
            playerInputManager.UpdateBuildingPrefab(gameObjectToBuild);
            _exitShopButtonScript.gameObject.GetComponent<Button>().onClick.Invoke();
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
            if (uiController)
            {
                // TODO: Add the ability to place prefabs.
            }
            
            // playerInputManager.GetItemObjectTileRange();
            
            // TODO: Run check after each placement, for availability on tiles and shop.
        }

        public Dictionary<Vector2Int, TileOverlay> ItemAndTileSize(int gameObjectToBuild)
        {
            var positionAndSize = new Dictionary<Vector2Int, TileOverlay>();
            // var itemToBuild = itemScrubs[gameObjectToBuild];
            var tileSizeX = 1; 
            var tileSizeY = 1; 
            
            if (tileSizeX == 0)
            {
                tileSizeX = 1;
            }
            if (tileSizeY == 0)
            {
                tileSizeY = 1;
            }
            
            for (int x = 0; x < tileSizeX; x++)
            {
                for (int y = 0; y < tileSizeY; y++)
                {
                    TileOverlay tile;
                    
                    // Use ShowTile() on every TileOverlay to indicate where we are placing...
                    // For that we require Vector2Int Position.
                    // TODO: Run the highlight on affected tiles over cursor.
                }
            }
            return positionAndSize;
        }
        
        /*private void PlaceRoad(Vector2Int position)
        {
            
        }

        private void FinishPlacingRoad()
        {
        
        }*/
        
    }
}
