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
        private Dictionary<Vector3Int, StructureModel> structureDictionary =
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
            else if (structureDictionary.ContainsKey(position))
            {
                structureDictionary[position].SwapModel(newModel, rotation);
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

        internal void RemoveAllTemporaryStructures()
        {
            foreach (var structure in temporaryRoadObjects.Values)
            {
                var position = Vector3Int.RoundToInt(structure.transform.position);
                _placementGrid[position.x, position.z] = CellType.Empty;
                Destroy(structure.gameObject);
            }
            temporaryRoadObjects.Clear();
        }

        internal List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
        {
            var resultPath = GridSearch.AStarSearch(_placementGrid, new Point(startPosition.x, startPosition.z),
                new Point(endPosition.x, endPosition.z));
            List<Vector3Int> path = new List<Vector3Int>();
            foreach (var point in resultPath)
            {
                path.Add(new Vector3Int(point.X,0,point.Y));
            }

            return path;
        }

        internal void AddTemporaryStructuresToStructureDictionary()
        {
            foreach (var structure in temporaryRoadObjects)
            {
                structureDictionary.Add(structure.Key, structure.Value);
                DestroyNatureAt(structure.Key);
            }
            temporaryRoadObjects.Clear();
        }

        internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type, int width = 1, int height = 1)
        {
            StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    var newPosition = position + new Vector3Int(x, 0, z);
                    _placementGrid[newPosition.x, newPosition.z] = type;
                    structureDictionary.Add(newPosition, structure);
                    DestroyNatureAt(newPosition);
                }                   
            }
        }

        private void DestroyNatureAt(Vector3Int position)
        {
            RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f),
                transform.up, Quaternion.identity, 1f, 1 << LayerMask.NameToLayer("Nature"));
            foreach (var item in hits)
            {
                Destroy(item.collider.gameObject);
            }
        }
    }
}
