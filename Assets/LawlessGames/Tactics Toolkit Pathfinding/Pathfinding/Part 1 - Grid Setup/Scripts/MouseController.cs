using System;
using System.Collections.Generic;
using System.Linq;
using LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_2___Grid_based_Pathfinding.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using CharacterInfo = LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_2___Grid_based_Pathfinding.Scripts.CharacterInfo;

namespace LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_1___Grid_Setup.Scripts
{
    public class MouseController : MonoBehaviour
    {
        public GameObject cursor;
        public float characterSpeed;
        public GameObject characterPrefab;
        private CharacterInfo _character;

        private PathFinder _pathFinder;
        private RangeFinder _rangeFinder;
        private List<OverlayTile> _path;
        private List<OverlayTile> _inRangeTiles = new List<OverlayTile>();
        private Camera _camera;

        // Update is called once per frame
        private void Start()
        {
            _camera = Camera.main;
            _pathFinder = new PathFinder();
            _rangeFinder = new RangeFinder();
            _path = new List<OverlayTile>();
        }

        private void GetInRangeTiles()
        {
            foreach (var item in _inRangeTiles)
            {
                item.HideTile();
            }   
            
            _inRangeTiles = _rangeFinder.GetTilesInRange(_character.activeTile, 3);

            foreach (var item in _inRangeTiles)
            {
                item.ShowTile();
            }
        }

        private void LateUpdate()
        {
            var focusedTileHit = GetFocusedOnTile();
            
            if (focusedTileHit.HasValue)
            {
                OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
                transform.position = overlayTile.transform.position;
                gameObject.GetComponent<SpriteRenderer>().sortingOrder =
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
                
                if (Input.GetMouseButtonDown(0))
                {
                    overlayTile.ShowTile();

                    if (_character == null)
                    {
                        _character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
                        PositionCharacterOnTile(overlayTile);
                        GetInRangeTiles();
                    }
                    else
                    {
                        _path = _pathFinder.FindPath(_character.activeTile, overlayTile);
                        
                        overlayTile.HideTile();
                    }
                }
            }

            if (_path.Count > 0)
            {
                MoveAlongPath();
            }
        }

        private void MoveAlongPath()
        {
            var step = characterSpeed * Time.deltaTime;

            var zIndex = _path[0].transform.position.z;
            var characterPosition = _character.transform.position;
            characterPosition = Vector2.MoveTowards(characterPosition, _path[0].transform.position, step);
            characterPosition = new Vector3(characterPosition.x, characterPosition.y, zIndex);
            _character.transform.position = characterPosition;

            if (Vector2.Distance(_character.transform.position, _path[0].transform.position) < 0.0001f)
            {
                PositionCharacterOnTile(_path[0]);  
                _path.RemoveAt(0);
            }

            if (_path.Count == 0)
            {
                GetInRangeTiles();
            }
        }

        private void PositionCharacterOnTile(OverlayTile tile)
        {
            var position = tile.transform.position;
            _character.transform.position = new Vector3(position.x, position.y+0.0001f,
                position.z);
            _character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            _character.activeTile = tile;
        }

        private static RaycastHit2D? GetFocusedOnTile()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

            // We raycast all and put them in a list, cuz it's not always clear what tile we want
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

            if(hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }
    }
}
