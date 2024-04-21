using System.Collections.Generic;
using System.Linq;
using finished3;
using UnityEngine;
using static finished3.ArrowTranslator;
using CharacterInfo = finished3.CharacterInfo;

namespace Iso_Pathfinding_Scripts
{
    public class MouseController : MonoBehaviour
    {
        public GameObject cursor;
        public float speed;
        public GameObject characterPrefab;
        public int movementRange = 3;
        private CharacterInfo _character;

        private PathFinder _pathFinder;
        private RangeFinder _rangeFinder;
        private ArrowTranslator _arrowTranslator;
        private List<OverlayTile> _path;
        private List<OverlayTile> _rangeFinderTiles;
        private bool _isMoving;

        private void Start()
        {
            _pathFinder = new PathFinder();
            _rangeFinder = new RangeFinder();
            _arrowTranslator = new ArrowTranslator();

            _path = new List<OverlayTile>();
            _isMoving = false;
            _rangeFinderTiles = new List<OverlayTile>();
        }

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                cursor.transform.position = tile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tile.transform.GetComponent<SpriteRenderer>().sortingOrder;

                if (_rangeFinderTiles.Contains(tile) && !_isMoving)
                {
                    _path = _pathFinder.FindPath(_character.standingOnTile, tile, _rangeFinderTiles);

                    foreach (var item in _rangeFinderTiles)
                    {
                        MapManager.Instance.Map[item.Grid2DLocation].SetSprite(ArrowDirection.None);
                    }

                    for (int i = 0; i < _path.Count; i++)
                    {
                        var previousTile = i > 0 ? _path[i - 1] : _character.standingOnTile;
                        var futureTile = i < _path.Count - 1 ? _path[i + 1] : null;

                        var arrow = _arrowTranslator.TranslateDirection(previousTile, _path[i], futureTile);
                        _path[i].SetSprite(arrow);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    tile.ShowTile();

                    if (_character == null)
                    {
                        _character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
                        PositionCharacterOnLine(tile);
                        GetInRangeTiles();
                    }
                    else
                    {
                        _isMoving = true;
                        tile.gameObject.GetComponent<OverlayTile>().HideTile();
                    }
                }
            }
            if (_path.Count > 0 && _isMoving)
            {
                MoveAlongPath();
            }
            if (_path.Count == 0 && _character != null)
            {
                GetInRangeTiles();
                _isMoving = false;
            }
        }

        private void MoveAlongPath()
        {
            var step = speed * Time.deltaTime;

            float zIndex = _path[0].transform.position.z;
            var position = _character.transform.position;
            position = Vector2.MoveTowards(position, _path[0].transform.position, step);
            position = new Vector3(position.x, position.y, zIndex);
            _character.transform.position = position;

            if (Vector2.Distance(_character.transform.position, _path[0].transform.position) < 0.00001f)
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

        private void PositionCharacterOnLine(OverlayTile tile)
        {
            var position = tile.transform.position;
            _character.transform.position = new Vector3(position.x, position.y + 0.0001f, position.z);
            _character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            _character.standingOnTile = tile;
        }

        // The "?" after RaycastHit2D makes it optional
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
            _rangeFinderTiles = _rangeFinder.GetTilesInRange(new Vector2Int(_character.standingOnTile.gridLocation.x, _character.standingOnTile.gridLocation.y), movementRange);

            foreach (var item in _rangeFinderTiles)
            {
                item.ShowTile();
            }
        }
    }
}
