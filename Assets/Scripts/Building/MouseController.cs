using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Building
{
    // From the old tutorial : )
    public class MouseController : MonoBehaviour
    {
        public GameObject cursor;
        public float speed;
        public GameObject buildingPrefab;
        public int movementRange = 3;
        private BuildingInfo _building;

        private DestinationFinder _destinationFinder;
        private TileRadiusFinder _tileRadiusFinder;
        private ArrowChanger _arrowChanger;
        private List<TileOverlay> _path;
        private List<TileOverlay> _rangeFinderTiles;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _destinationFinder = new DestinationFinder();
            _tileRadiusFinder = new TileRadiusFinder();
            _arrowChanger = new ArrowChanger();

            _path = new List<TileOverlay>();
            _rangeFinderTiles = new List<TileOverlay>();
        }

        void LateUpdate()
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Benjamin Test 4"))
            {
                CheckClickDown();
            }
        }

        private void CheckClickDown()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
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

                    if (tile.typeOfTheTile == TileType.Empty)
                    {
                        _building = Instantiate(buildingPrefab).GetComponent<BuildingInfo>();
                        PositionCharacterOnLine(tile);
                        tile.gameObject.GetComponent<TileOverlay>().HideTile();
                        tile.typeOfTheTile = TileType.Building;
                    }
                }
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
        }

        private void PositionCharacterOnLine(TileOverlay tile)
        {
            var position = tile.transform.position;
            _building.transform.position = new Vector3(position.x, position.y + 0.0001f, position.z);
            _building.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            _building.standingOnTile = tile;
        }

        private RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }

        private void GetInRangeTiles()
        {
            _rangeFinderTiles = _tileRadiusFinder.GetTilesInRange(new Vector2Int(_building.standingOnTile.gridLocation.x, _building.standingOnTile.gridLocation.y), movementRange);

            foreach (var item in _rangeFinderTiles)
            {
                item.ShowTile();
            }
        }
    }
}
