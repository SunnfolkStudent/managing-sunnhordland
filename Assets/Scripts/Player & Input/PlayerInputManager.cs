using System;
using System.Collections.Generic;
using System.Linq;
using Building;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

namespace Player___Input
{
    public class PlayerInputManager : MonoBehaviour
    {
        // TODO: Get these Actions working...
        // public UnityEvent OnPointerStay;
        public Action OnMouseHover;
        public Action<Vector2Int> OnMouseClick, OnMouseHold;
        public Action OnMouseUp;

        private PlayerControls _playerControls;
        
        // private Vector2 _cameraMovementVector; 
        // public Vector2 cameraMovementVector => _cameraMovementVector;
        
        public GameObject cursor;
        public GameObject buildingPrefab;
        public int movementRange = 3;
        private BuildingInfo _building;

        private DestinationFinder _destinationFinder;
        private TileRadiusFinder _tileRangeFinder;
        private ArrowChanger _arrowChanger;
        private List<TileOverlay> _path;
        private List<TileOverlay> _rangeFinderTiles;
        private Camera _camera;

        public Vector2 MoveCamera { get; private set; }

        [SerializeField] private int uiLayer;
        
        private Dictionary<GameObject, int> _placedBuildings = new Dictionary<GameObject, int>();
        public static bool IsMouseOverUi
        {
            get
            { 
                // [Works with PhysicsRaycaster on the Camera. Requires New Input System. Assumes mouse.)
                if (EventSystem.current == null)
                {
                    return false;
                }
                RaycastResult lastRaycastResult = ((InputSystemUIInputModule)EventSystem.current.currentInputModule).GetLastRaycastResult(Mouse.current.deviceId);
                const int uiLayer = 5;
                return lastRaycastResult.gameObject != null && lastRaycastResult.gameObject.layer == uiLayer;
            }
        }

        private void Awake() => _playerControls = new PlayerControls();

        public void OnEnable()
        {
            _playerControls.Enable();
        }

        public void OnDisable()
        {
            _playerControls.Disable();
        }

        private void Start()
        {
            uiLayer = LayerMask.NameToLayer("UI"); 
            _camera = Camera.main;
            _destinationFinder = new DestinationFinder();
            _tileRangeFinder = new TileRadiusFinder();
            _arrowChanger = new ArrowChanger();

            _path = new List<TileOverlay>();
            _rangeFinderTiles = new List<TileOverlay>();
        }

        private void Update()
        {
            MoveCamera = _playerControls.InGame.CameraMovements.ReadValue<Vector2>();
        }
        
        private void PanCamera(Vector3 dragOrigin)
        {
            if (_playerControls.InGame.LeftClick.WasPressedThisFrame())
            {
                dragOrigin = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            }
     
            if (_playerControls.InGame.LeftClick.IsPressed())
            {
                Vector3 difference = dragOrigin - _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _camera.transform.position += difference;
            }
        }


        /*#region UI-stuff
 
        private void Update()
        {
            if (IsPointerOverUIElement())
            {
                OnPointerStay?.Invoke();
            }
        }
 
 
        //Returns 'true' if we touched or hovering on Unity UI element.
        private bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        
        //Returns 'true' if we touched or hovering on Unity UI element.
        //Returns 'true' if we touched or hovering on Unity UI element.
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                RaycastResult curRaycastResult = eventSystemRaycastResults[index];
                if (curRaycastResult.gameObject.layer == uiLayer && curRaycastResult.gameObject == this.gameObject)
                    return true;
            }
            return false;
        }
 
        //Gets all event system raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults;
        }
        
        #endregion*/

        void LateUpdate()
        {
            if (IsMouseOverUi)
            {
                Debug.Log("mouse is detecting UI");
            }

            if (!IsMouseOverUi && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Benjamin Test 4"))
            {
                if (BuildManager.InBuildMode)
                {
                    //GetItemObjectTileRange();
                    CheckClickDownBuildMode();
                }
                else
                {
                    CheckClickDown();
                }
            }
        }

        private void CheckClickDown()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue && hit.Value.collider.gameObject.GetComponent<TileOverlay>())
            {
                TileOverlay tile = hit.Value.collider.gameObject.GetComponent<TileOverlay>();
                cursor.transform.position = tile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                    tile.transform.GetComponent<SpriteRenderer>().sortingOrder;

                if (_rangeFinderTiles.Contains(tile))
                {
                    _path = _destinationFinder.FindPath(_building.standingOnTile, tile, _rangeFinderTiles);

                    foreach (var item in _rangeFinderTiles)
                    {
                        GridManager.Instance.TileOverlayMap[item.Grid2DLocation].SetSprite(ArrowChanger.ArrowDirection.None);
                    }

                    for (int i = 0; i < _path.Count; i++)
                    {
                        var previousTile = i > 0 ? _path[i - 1] : _building.standingOnTile;
                        var futureTile = i < _path.Count - 1 ? _path[i + 1] : null;

                        var arrow = _arrowChanger.TranslateDirection(previousTile, _path[i], futureTile);
                        _path[i].SetSprite(arrow);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    tile.ShowTile();

                    if (tile.typeOfTheTile == TileType.Empty && BuildManager.InBuildMode)
                    {
                        _building = Instantiate(buildingPrefab).GetComponent<BuildingInfo>();
                        PositionBuildingOnLine(tile);
                        tile.gameObject.GetComponent<TileOverlay>().HideTile();
                        tile.typeOfTheTile = TileType.Building;
                    }
                }
            }
        }

        /*private void MoveAlongPath()
        {
            var step = speed * Time.deltaTime;

            float zIndex = _path[0].transform.position.z;
            var position = _building.transform.position;
            position = Vector2.MoveTowards(position, _path[0].transform.position, step);
            position = new Vector3(position.x, position.y, zIndex);
            _building.transform.position = position;

            if (Vector2.Distance(_building.transform.position, _path[0].transform.position) < 0.00001f)
            {
                PositionCharacterOnLine(_path[0]);
                _path.RemoveAt(0);
            }

        }*/

        private void PositionBuildingOnLine(TileOverlay tile)
        {
            var position = tile.transform.position;
            _building.transform.position = new Vector3(position.x, position.y + 0.0001f, position.z);
            _building.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            _building.standingOnTile = tile;
        }

        // The "?" after RaycastHit2D means that RaycastHit2D is Nullable
        // It means it can return either a Vector2 from Raycast2D or null.
        private RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            // Changing to Vector2, cuz we 2D isometric, not 3D.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            PanCamera(mousePos);

            // Using RaycastAll with a list, cuz it's more reliable in finding the right tile
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }

        private void GetAdjacentTilesInRange()
        {
            _rangeFinderTiles = _tileRangeFinder.GetTilesInRange(new Vector2Int(_building.standingOnTile.gridLocation.x, _building.standingOnTile.gridLocation.y), movementRange);

            foreach (var item in _rangeFinderTiles)
            {
                item.ShowTile();
            }
        }
        
        // TODO: Fix the below code, it has an offset from the mousePos.
        public void GetItemObjectTileRange()
        {
            RaycastHit2D? hit = GetFocusedOnTile();
            if (hit.HasValue)
            {
                TileOverlay tile = hit.Value.collider.gameObject.GetComponent<TileOverlay>();
                cursor.transform.position = tile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                    tile.transform.GetComponent<SpriteRenderer>().sortingOrder;
                
                var mousePos = cursor.transform.position;
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                Vector2Int mousePos2DVector2Int = Vector2Int.RoundToInt(mousePos2D);
                Debug.Log("MousePosV2 to Int: " + mousePos2DVector2Int);
                                
                _rangeFinderTiles = _tileRangeFinder.GetSquareTilesInRange(
                    new Vector2Int(mousePos2DVector2Int.x, mousePos2DVector2Int.y), movementRange);
            }
                
            foreach (var item in _rangeFinderTiles)
            {
                item.ShowTile();
            }
        }
        
        private void CheckClickDownBuildMode()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                TileOverlay tile = hit.Value.collider.gameObject.GetComponent<TileOverlay>();
                cursor.transform.position = tile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                    tile.transform.GetComponent<SpriteRenderer>().sortingOrder;
                
                var mousePos = cursor.transform.position;
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                Vector2Int mousePos2DVector2Int = Vector2Int.RoundToInt(mousePos2D);
                Debug.Log("MousePosV2 to Int: " + mousePos2DVector2Int);
                
                _rangeFinderTiles = _tileRangeFinder.GetTilesInRange(
                    new Vector2Int(mousePos2DVector2Int.x, mousePos2DVector2Int.y), movementRange);
                
                tile.ShowTile();

                if (Input.GetMouseButtonDown(0))
                {
                    if (tile.typeOfTheTile == TileType.Empty && BuildManager.InBuildMode)
                    {
                        _building = Instantiate(buildingPrefab).GetComponent<BuildingInfo>();
                        PositionBuildingOnLine(tile);
                        tile.gameObject.GetComponent<TileOverlay>().HideTile();
                        tile.typeOfTheTile = TileType.Building;
                    } 
                }
            }
        }
        
    }
}
