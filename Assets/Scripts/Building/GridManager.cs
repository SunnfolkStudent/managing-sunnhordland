using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Building
{
    public class GridManager : MonoBehaviour
    {
        // Singleton MapManager   
        private static GridManager _instance;
        public static GridManager Instance => _instance;

        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        public Dictionary<Vector2Int, TileOverlay> TileOverlayMap;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            } 
            else
            {
                _instance = this;
            }
        }
        
        void Start()
        {
            var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
            TileOverlayMap = new Dictionary<Vector2Int, TileOverlay>();

            // Retrieves any tilemaps from children, and checks their bounds.
            foreach (var tilemap in tileMaps)
            {
                BoundsInt bounds = tilemap.cellBounds;

                // We start with checking height (z), to avoid checking more tiles than we need to
                for (int z = bounds.max.z; z > bounds.min.z; z--)
                {
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            // Checking if there are holes in the tilemap not accounted for.
                            if (tilemap.HasTile(new Vector3Int(x, y, z)))
                            {
                                // If the map doesn't already have a key (Vector2Int) there, create one. 
                                if (!TileOverlayMap.ContainsKey(new Vector2Int(x, y)))
                                {
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    var cellWorldPosition = tilemap.GetCellCenterWorld(new Vector3Int(x, y, z));
                                    
                                    // We're increasing it by z + 1, cuz this is for creating overlays on top of tiles.
                                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                                    overlayTile.gameObject.GetComponent<TileOverlay>().gridLocation = new Vector3Int(x, y, z);
                                    overlayTile.gameObject.GetComponent<TileOverlay>().typeOfTheTile = TileType.Empty;
    
                                    // Grants each individual tile a unique key, and assigns the TileOverlay value, which is a class all tiles have as a component.
                                    TileOverlayMap.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<TileOverlay>());
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<TileOverlay> GetSurroundingTiles(Vector2Int originTile)
        {
            var surroundingTiles = new List<TileOverlay>();

            Vector2Int tileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }
            return surroundingTiles;
        }
        
        public List<TileOverlay> GetSurroundingSquareTiles(Vector2Int originTile)
        {
            var surroundingTiles = new List<TileOverlay>();

            Vector2Int tileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }
            
            tileToCheck = new Vector2Int(originTile.x + 1, originTile.y + 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }
            
            tileToCheck = new Vector2Int(originTile.x + 1, originTile.y - 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }
            
            tileToCheck = new Vector2Int(originTile.x - 1, originTile.y + 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }
            
            tileToCheck = new Vector2Int(originTile.x - 1, originTile.y - 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
            if (TileOverlayMap.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(TileOverlayMap[tileToCheck].transform.position.z - TileOverlayMap[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(TileOverlayMap[tileToCheck]);
            }
            return surroundingTiles;
        }
    }
}
