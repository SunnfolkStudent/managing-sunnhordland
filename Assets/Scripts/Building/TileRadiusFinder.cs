using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Building
{
    public class TileRadiusFinder
    {
        public List<TileOverlay> GetTilesInRange(Vector2Int location, int range)
        {
            var startingTile = GridManager.Instance.TileOverlayMap[location];
            var inRangeTiles = new List<TileOverlay>();
            int stepCount = 0;

            inRangeTiles.Add(startingTile);

            // Should contain the surroundingTiles of the previous step. 
            var tilesForPreviousStep = new List<TileOverlay>();
            tilesForPreviousStep.Add(startingTile);
            while (stepCount < range)
            {
                var surroundingTiles = new List<TileOverlay>();

                foreach (var item in tilesForPreviousStep)
                {
                    surroundingTiles.AddRange(GridManager.Instance.GetSurroundingTiles(new Vector2Int(item.gridLocation.x, item.gridLocation.y)));
                }

                inRangeTiles.AddRange(surroundingTiles);
                tilesForPreviousStep = surroundingTiles.Distinct().ToList();
                stepCount++;
            }

            return inRangeTiles.Distinct().ToList();
        }
        
        public List<TileOverlay> GetSquareTilesInRange(Vector2Int location, int range)
        {
            var startingTile = GridManager.Instance.TileOverlayMap[location];
            var inRangeTiles = new List<TileOverlay>();
            int stepCount = 0;

            inRangeTiles.Add(startingTile);

            // Should contain the surroundingTiles of the previous step. 
            var tilesForPreviousStep = new List<TileOverlay>();
            tilesForPreviousStep.Add(startingTile);
            while (stepCount < range)
            {
                var surroundingTiles = new List<TileOverlay>();

                foreach (var item in tilesForPreviousStep)
                {
                    surroundingTiles.AddRange(GridManager.Instance.GetSurroundingSquareTiles(new Vector2Int(item.gridLocation.x, item.gridLocation.y)));
                }

                inRangeTiles.AddRange(surroundingTiles);
                tilesForPreviousStep = surroundingTiles.Distinct().ToList();
                stepCount++;
            }

            return inRangeTiles.Distinct().ToList();
        }
        
    }
}
