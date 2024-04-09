using System.Collections.Generic;
using Benjamin_Test.City_Builder_Test.Scripts;
using SVS;
using UnityEngine;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class RoadManager : MonoBehaviour
    {
        public PlacementManager placementManager;

        public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
        public List<Vector3Int> roadPositionToRecheck = new List<Vector3Int>();

        private Vector3Int _startPosition;
        private bool _placementMode = false;
        
        public RoadFixer roadFixer;

        private void Start()
        {
            roadFixer = GetComponent<RoadFixer>();
        }

        public void PlaceRoad(Vector3Int position)
        {
            if (placementManager.CheckIfPositionInbound(position) == false)
                return;
            if (placementManager.CheckIfPositionIsFree(position) == false)
                return;
            if (_placementMode == false)
            {
                temporaryPlacementPositions.Clear();
                roadPositionToRecheck.Clear();
                _placementMode = true;
                _startPosition = position;
                temporaryPlacementPositions.Add(position);
                placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);
            }
            else
            {
                placementManager.RemoveAllTemporaryStructures();
                temporaryPlacementPositions.Clear();
                roadPositionToRecheck.Clear();

                temporaryPlacementPositions = placementManager.GetPathBetween(_startPosition, position);

                foreach (var temporaryPosition in temporaryPlacementPositions)
                {
                    placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);
                }
            }
            
            FixRoadPrefabs();
        }

        private void FixRoadPrefabs()
        {
            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
                var neighbours = placementManager.GetNeighboursOfTypeFor(temporaryPosition, CellType.Road);
                // TODO: The below foreach loop can be refactored (See comment on road.bugfix 3 video):
                foreach (var roadPosition in neighbours)
                {
                    if (roadPositionToRecheck.Contains(roadPosition) == false)
                    {
                        roadPositionToRecheck.Add(roadPosition);
                    }
                }
            }

            foreach (var positionToFix in roadPositionToRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, positionToFix);
            }
        }

        public void FinishPlacingRoad()
        {
            _placementMode = false;
            placementManager.AddTemporaryStructuresToStructureDictionary();
            if (temporaryPlacementPositions.Count > 0)
            {
                AudioPlayer.instance.PlayPlacementSound();
            }
            temporaryPlacementPositions.Clear();
            _startPosition = Vector3Int.zero;
        }
    }
}
