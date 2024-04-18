using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_1___Grid_Setup.Scripts
{
    public class RangeFinder
    {
        public List<OverlayTile> GetTilesInRange(OverlayTile startingTile, int rangeTile)
        {
            var inRangeTiles = new List<OverlayTile>();
            int stepCount = 0;
            
            inRangeTiles.Add(startingTile);

            var tileForPreviousStep = new List<OverlayTile>();
            tileForPreviousStep.Add(startingTile);

            while (stepCount < rangeTile)
            {
                var surroundingTiles = new List<OverlayTile>();

                foreach (var item in tileForPreviousStep)
                {
                    surroundingTiles.AddRange(MapManager.Instance.GetNeighbourTiles(item));
                }
                
                inRangeTiles.AddRange(surroundingTiles);
                tileForPreviousStep = surroundingTiles.Distinct().ToList();
                stepCount++;
            }

            return inRangeTiles.Distinct().ToList();
        }
    }
}
