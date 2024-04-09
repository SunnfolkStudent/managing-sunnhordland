using System;
using System.Linq;
using SVS;
using UnityEngine;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class StructureManager : MonoBehaviour
    {
        public StructurePrefabWeighted[] housePrefabs, specialPrefabs, bigStructuresPrefabs;
        public PlacementManager placementManager;

        private float[] _houseWeights, _specialWeights, _bigStructureWeights;

        private void Start()
        {
            _houseWeights = housePrefabs.Select(prefabStats => prefabStats.weight).ToArray();
            _specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
            _bigStructureWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        }

        public void PlaceHouse(Vector3Int position)
        {
            if (CheckPositionBeforePlacement(position))
            {
                int randomIndex = GetRandomWeightedIndex(_houseWeights);
                placementManager.PlaceObjectOnTheMap(position, housePrefabs[randomIndex].prefab, CellType.Structure);
                AudioPlayer.instance.PlayPlacementSound();
            }
        }
        
        public void PlaceSpecial(Vector3Int position)
        {
            if (CheckPositionBeforePlacement(position))
            {
                int randomIndex = GetRandomWeightedIndex(_specialWeights);
                placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.Structure);
                AudioPlayer.instance.PlayPlacementSound();
            }
        }
        
        internal void PlaceBigStructure(Vector3Int position)
        {
            int width = 2;
            int height = 2;
            if (CheckBigStructure(position, width, height))
            {
                int randomIndex = GetRandomWeightedIndex(_bigStructureWeights);
                placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, CellType.Structure, width, height);
                AudioPlayer.instance.PlayPlacementSound();
            }
        }

        private int GetRandomWeightedIndex(float[] weights)
        {
            float sum = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i];
            }

            float randomValue = UnityEngine.Random.Range(0, sum);
            float tempSum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                if (randomValue >= tempSum && randomValue < tempSum + weights[i])
                {
                    return i;
                }
                tempSum += weights[i];
            }

            return 0;
        }

        private bool CheckPositionBeforePlacement(Vector3Int position)
        {
            if (DefaultCheck(position) == false)
            {
                return false;
            }

            if (RoadCheck(position) == false)
            {
                return false;
            }

            return true;
        }

        private bool RoadCheck(Vector3Int position)
        {
            if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
            {
                Debug.Log("Must be placed near a road");
                return false;
            }
            return true;
        }

        private bool DefaultCheck(Vector3Int position)
        {
            if (placementManager.CheckIfPositionInbound(position) == false)
            {
                Debug.Log("This position is out of bounds");
                return false;
            }

            if (placementManager.CheckIfPositionIsFree(position) == false)
            {
                Debug.Log("This position is not empty");
                return false;
            }
            return true;
        }

        private bool CheckBigStructure(Vector3Int position, int width, int height)
        {
            var nearRoad = false;
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    var newPosition = position + new Vector3Int(x, 0, z);
                    
                    if (DefaultCheck(newPosition) == false)
                    {
                        return false;
                    }
                    if (nearRoad == false)
                    {
                        nearRoad = RoadCheck(newPosition);
                    }
                }
            }

            return nearRoad;
        }
    }

    [Serializable]
    public struct StructurePrefabWeighted
    {
        public GameObject prefab;
        [Range(0, 1)] public float weight;
    }
    
}
