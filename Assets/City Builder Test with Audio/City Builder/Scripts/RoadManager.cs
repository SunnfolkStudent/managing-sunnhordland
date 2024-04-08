using System;
using System.Collections.Generic;
using UnityEngine;

namespace Benjamin_Test.City_Builder_Test.Scripts
{
    public class RoadManager : MonoBehaviour
    {
        public PlacementManager placementManager;

        public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
        public List<Vector3Int> roadPositionToRecheck = new List<Vector3Int>();
        public GameObject roadStraight;

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
            temporaryPlacementPositions.Clear();
            temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
            FixRoadPrefabs();
        }

        private void FixRoadPrefabs()
        {
            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
                var neighbours = placementManager.GetNeighboursOfTypeFor(temporaryPosition, CellType.Road);
                foreach (var roadPosition in neighbours)
                {
                    roadPositionToRecheck.Add(roadPosition);
                }
            }

            foreach (var positionToFix in roadPositionToRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, positionToFix);
            }
        }
    }
}
