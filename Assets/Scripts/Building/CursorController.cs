using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Building.ArrowChanger;
using BuildingInfo = Building.BuildingInfo;

namespace Building
{
    public class CursorController : MonoBehaviour
    {
        public GameObject cursor;
        public float speed;
        public GameObject buildingPrefab;
        public int movementRange = 10;
        private BuildingInfo _building;

        private DestinationFinder _destinationFinder;
        private TileRadiusFinder _rangeFinder;
        private ArrowChanger _arrowChanger;
        private List<TileOverlay> _path;
        private List<TileOverlay> _tileRadiusFinderTiles;
        private bool _characterIsMoving;

        private void Start()
        {
            _destinationFinder = new DestinationFinder();
            _rangeFinder = new TileRadiusFinder();
            _arrowChanger = new ArrowChanger();

            _path = new List<TileOverlay>();
            _characterIsMoving = false;
            _tileRadiusFinderTiles = new List<TileOverlay>();
        }

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                TileOverlay tileOverlay = hit.Value.collider.gameObject.GetComponent<TileOverlay>();
                cursor.transform.position = tileOverlay.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tileOverlay.transform.GetComponent<SpriteRenderer>().sortingOrder;

                if (_tileRadiusFinderTiles.Contains(tileOverlay) && !_characterIsMoving)
                {
                    _path = _destinationFinder.FindPath(_building.standingOnTile, tileOverlay, _tileRadiusFinderTiles);

                    foreach (var item in _tileRadiusFinderTiles)
                    {
                        GridManager.Instance.Map[item.Grid2DLocation].SetSprite(ArrowDirection.None);
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
                    tileOverlay.ShowTile();

                    if (_building == null)
                    {
                        _building = Instantiate(buildingPrefab).GetComponent<BuildingInfo>();
                        PositionCharacterOnLine(tileOverlay);
                        GetInRangeTiles();
                    }
                    else
                    {
                        _characterIsMoving = true;
                        tileOverlay.gameObject.GetComponent<TileOverlay>().HideTile();
                    }
                }
            }
            if (_path.Count > 0 && _characterIsMoving)
            {
                MoveAlongPath();
            }
            if (_path.Count == 0 && _building != null)
            {
                GetInRangeTiles();
                _characterIsMoving = false;
            }
        }

        private void MoveAlongPath()
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

            /*if (path.Count == 0)
            {
                GetInRangeTiles();
                isMoving = false;
            }*/

        }

        private void PositionCharacterOnLine(TileOverlay overlayTile)
        {
            var position = overlayTile.transform.position;
            _building.transform.position = new Vector3(position.x, position.y + 0.0001f, position.z);
            _building.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            _building.standingOnTile = overlayTile;
        }

        // The "?" after RaycastHit2D means that RaycastHit2D is Nullable
        // It means it can return either a Vector2 from Raycast2D or null.
        private static RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Changing to Vector2, cuz we 2D isometric, not 3D.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Using RaycastAll with a list, cuz it's more reliable in finding the right tile
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }

        private void GetInRangeTiles()
        {
            _tileRadiusFinderTiles = _rangeFinder.GetTilesInRange(new Vector2Int(_building.standingOnTile.gridLocation.x, _building.standingOnTile.gridLocation.y), movementRange);

            foreach (var item in _tileRadiusFinderTiles)
            {
                item.ShowTile();
            }
        }
    }
}
