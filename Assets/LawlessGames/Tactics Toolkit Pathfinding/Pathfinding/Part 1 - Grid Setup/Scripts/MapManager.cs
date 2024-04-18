using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_1___Grid_Setup.Scripts
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance => _instance;

        public OverlayTile overlayTilePrefab;
        public GameObject overlayContainer;

        public float littleBump;

        // The Dictionary helps up select the uppermost tile of tiles on the same z.pos.
        public Dictionary<Vector2Int, OverlayTile> Map;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            littleBump = 0.0003f;
            var tileMap = gameObject.GetComponentInChildren<Tilemap>();
            Map = new Dictionary<Vector2Int, OverlayTile>();

            BoundsInt bounds = tileMap.cellBounds;

            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tileMap.HasTile(tileLocation) && !Map.ContainsKey(tileKey))
                        {
                            var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                            var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                            
                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                            overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                            overlayTile.gridLocation = tileLocation;
                            Map.Add(tileKey, overlayTile);
                        }
                    }
                }
            }
        }
        
        public List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile)
        {
            var map = MapManager.Instance.Map;
            List<OverlayTile> neighbours = new List<OverlayTile>();
            
            // Top neighbour
            Vector2Int locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, currentOverlayTile.gridLocation.y + 1);

            if (map.ContainsKey(locationToCheck))
            {
                if(Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= 1)
                    neighbours.Add(map[locationToCheck]);
            }
            
            // Bottom neighbour
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, currentOverlayTile.gridLocation.y - 1);

            if (map.ContainsKey(locationToCheck))
            {
                if(Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= 1)
                    neighbours.Add(map[locationToCheck]);
            }
            
            // Right neighbour
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + 1, currentOverlayTile.gridLocation.y);

            if (map.ContainsKey(locationToCheck))
            {
                if(Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= 1)
                    neighbours.Add(map[locationToCheck]);
            }
            
            // Left neighbour
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x - 1, currentOverlayTile.gridLocation.y);

            if (map.ContainsKey(locationToCheck))
            {
                if(Mathf.Abs(currentOverlayTile.gridLocation.z - map[locationToCheck].gridLocation.z) <= 1)
                    neighbours.Add(map[locationToCheck]);
            }

            return neighbours;
        }
        
    }
}
