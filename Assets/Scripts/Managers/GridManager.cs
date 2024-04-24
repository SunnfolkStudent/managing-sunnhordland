using System.Collections.Generic;
using System.Linq;
using Building;
using Iso_Pathfinding_Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        // Singleton GridManager
        private static GridManager _instance;
        public static GridManager Instance => _instance;

        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        public Dictionary<Vector2Int, OverlayTile> Map;

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
            GenerateInitialTilemap();
        }

        private void GenerateInitialTilemap()
        {
            var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>()
                .OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
            Map = new Dictionary<Vector2Int, OverlayTile>();

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
                                if (!Map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    var cellWorldPosition = tilemap.GetCellCenterWorld(new Vector3Int(x, y, z));
                                    
                                    // We're increasing the cellWorldPosition.z by +1, cuz we want overlay on top of the tiles we're hovering our mouse on.
                                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y,
                                        cellWorldPosition.z + 1);
                                    
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder =
                                        tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                                    overlayTile.gameObject.GetComponent<OverlayTile>().gridLocation = new Vector3Int(x, y, z);

                                    Map.Add(new Vector2Int(x, y), overlayTile.gameObject.GetComponent<OverlayTile>());
                                }
                            }
                        }
                    }
                }
            }
        }

        /*public List<OverlayTile> GetSurroundingTiles(Vector2Int originTile)
        {
            var surroundingTiles = new List<OverlayTile>();


            Vector2Int tileToCheck = new Vector2Int(originTile.x + 1, originTile.y);
            if (Map.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(Map[tileToCheck].transform.position.z - Map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(Map[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x - 1, originTile.y);
            if (Map.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(Map[tileToCheck].transform.position.z - Map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(Map[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x, originTile.y + 1);
            if (Map.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(Map[tileToCheck].transform.position.z - Map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(Map[tileToCheck]);
            }

            tileToCheck = new Vector2Int(originTile.x, originTile.y - 1);
            if (Map.ContainsKey(tileToCheck))
            {
                if (Mathf.Abs(Map[tileToCheck].transform.position.z - Map[originTile].transform.position.z) <= 1)
                    surroundingTiles.Add(Map[tileToCheck]);
            }

            return surroundingTiles;
        }*/
    }
}

// You can create a grid array with all tiles, and e.g. you want to fetch tile 8, so you do like this:

public class GridTest
{
    private int[] _xArray = new int[100]; // (where I put 100, you put the max amounts of ints you want in the array)

    public int this[int chosenNumber]
    {
        get => _xArray[chosenNumber];
        set => _xArray[chosenNumber] = value;
    }
}

public class RetrievingATile : MonoBehaviour
{
    private GridTest _grid = new GridTest();

    private void Start()
    {
        var findXTile = _grid[10];
        Debug.Log(findXTile);
    }
}