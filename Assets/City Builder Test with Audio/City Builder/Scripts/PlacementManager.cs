using System;
using System.Collections.Generic;
using UnityEngine;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class PlacementManager : MonoBehaviour
    {
        public int width, height;
        private Grid _placementGrid;

        private Dictionary<Vector3Int, StructureModel> temporaryRoadObjects =
            new Dictionary<Vector3Int, StructureModel>();

        private void Start()
        {
            _placementGrid = new Grid(width, height);
        }

        internal bool CheckIfPositionInbound(Vector3Int position)
        {
            if (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height)
            {
                return true;
            }

            return false;
        }

        internal bool CheckIfPositionIsFree(Vector3Int position)
        {
            return CheckIfPositionIsOfType(position, CellType.Empty);
        }

        private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
        {
            return _placementGrid[position.x, position.z] == type;
        }

        internal void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
        {
            _placementGrid[position.x, position.z] = type;
            StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
            temporaryRoadObjects.Add(position, structure);
        }

        private StructureModel CreateANewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
        {
            GameObject structure = new GameObject(type.ToString());
            structure.transform.SetParent(transform);
            structure.transform.localPosition = position;
            var structureModel = structure.AddComponent<StructureModel>();
            structureModel.CreateModel(structurePrefab);
            return structureModel;
        }

        public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
        {
            if (temporaryRoadObjects.ContainsKey(position))
            {
                temporaryRoadObjects[position].SwapModel(newModel, rotation);
            }
        }

        internal CellType[] GetNeighbourTypesFor(Vector3Int position)
        {
            return _placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
        }

        internal List<Vector3Int> GetNeighboursOfTypeFor(Vector3Int position, CellType type)
        {
            var neighbourVertices =
                _placementGrid.GetAdjacentCellsOfType(position.x, position.z, type);
            var neighbours = new List<Vector3Int>();
            
            // TODO: The below foreach loop can be refactored (See comment on road.bugfix 3 video):
            foreach (var point in neighbourVertices)
            {
                neighbours.Add(new Vector3Int(point.X, 0, point.Y));
            }

            return neighbours;
        }

        public void RemoveAllTemporaryStructures()
        {
            throw new NotImplementedException();
        }

        public List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int position)
        {
            throw new NotImplementedException();
        }

        public void AddTemporaryStructuresToStructureDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
