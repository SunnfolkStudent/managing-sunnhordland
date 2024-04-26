using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public enum TileType
    {
        Empty,
        Road,
        Building,
        Nature
    }
    
    public class TileOverlay : MonoBehaviour
    {
        // These 3 public ints are for pathfinding mathematics, on calculating shortest route.
        public int G;
        public int H;
        public int F => G + H;

        public bool isBlocked = false;

        public TileType typeOfTheTile;
        public TileOverlay previous;
        public Vector3Int gridLocation;
        public Vector2Int Grid2DLocation => new(gridLocation.x, gridLocation.y);

        public List<Sprite> arrows;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HideTile();
            }
        }

        public void HideTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }

        public void ShowTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public void SetSprite(ArrowChanger.ArrowDirection direction)
        {
            if (direction == ArrowChanger.ArrowDirection.None)
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 0);
            else
            {
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = arrows[(int)direction];
                GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }

    }
}
