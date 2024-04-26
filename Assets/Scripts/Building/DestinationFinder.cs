using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Building
{
    public class DestinationFinder
    {
        private Dictionary<Vector2Int, TileOverlay> _searchableTiles;

        public List<TileOverlay> FindPath(TileOverlay start, TileOverlay end, List<TileOverlay> inRangeTiles)
        {
            _searchableTiles = new Dictionary<Vector2Int, TileOverlay>();

            List<TileOverlay> openList = new List<TileOverlay>();
            HashSet<TileOverlay> closedList = new HashSet<TileOverlay>();

            if (inRangeTiles.Count > 0)
            {
                foreach (var item in inRangeTiles)
                {
                    _searchableTiles.Add(item.Grid2DLocation, GridManager.Instance.TileOverlayMap[item.Grid2DLocation]);
                }
            }
            else
            {
                _searchableTiles = GridManager.Instance.TileOverlayMap;
            }

            openList.Add(start);

            while (openList.Count > 0)
            {
                TileOverlay currentOverlayTile = openList.OrderBy(x => x.F).First();

                openList.Remove(currentOverlayTile);
                closedList.Add(currentOverlayTile);

                if (currentOverlayTile == end)
                {
                    return GetFinishedList(start, end);
                }

                foreach (var tile in GetNeighbourOverlayTiles(currentOverlayTile))
                {
                    if (tile.isBlocked || closedList.Contains(tile) || Mathf.Abs(currentOverlayTile.transform.position.z - tile.transform.position.z) > 1)
                    {
                        continue;
                    }

                    tile.G = GetManhattanDistance(start, tile);
                    tile.H = GetManhattanDistance(end, tile);

                    tile.previous = currentOverlayTile;


                    if (!openList.Contains(tile))
                    {
                        openList.Add(tile);
                    }
                }
            }

            return new List<TileOverlay>();
        }

        private List<TileOverlay> GetFinishedList(TileOverlay start, TileOverlay end)
        {
            List<TileOverlay> finishedList = new List<TileOverlay>();
            TileOverlay currentTile = end;

            while (currentTile != start)
            {
                finishedList.Add(currentTile);
                currentTile = currentTile.previous;
            }

            finishedList.Reverse();

            return finishedList;
        }

        private int GetManhattanDistance(TileOverlay start, TileOverlay tile)
        {
            return Mathf.Abs(start.gridLocation.x - tile.gridLocation.x) + Mathf.Abs(start.gridLocation.y - tile.gridLocation.y);
        }

        private List<TileOverlay> GetNeighbourOverlayTiles(TileOverlay currentOverlayTile)
        {
            var map = GridManager.Instance.TileOverlayMap;

            List<TileOverlay> neighbours = new List<TileOverlay>();

            // right
            Vector2Int locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x + 1,
                currentOverlayTile.gridLocation.y
            );

            if (_searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(_searchableTiles[locationToCheck]);
            }

            // left
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x - 1,
                currentOverlayTile.gridLocation.y
            );

            if (_searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(_searchableTiles[locationToCheck]);
            }

            // top
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y + 1
            );

            if (_searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(_searchableTiles[locationToCheck]);
            }

            // bottom
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y - 1
            );

            if (_searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(_searchableTiles[locationToCheck]);
            }

            return neighbours;
        }

      
    }
}