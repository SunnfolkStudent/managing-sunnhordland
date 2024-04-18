using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_3___RangeFinding_and_Path_Display.Scripts.ArrowTranslator;

namespace LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_3___RangeFinding_and_Path_Display.Scripts
{
    public class OverlayTile : MonoBehaviour
    {
        public int G;
        public int H;
        public int F => G + H;

        public bool isBlocked = false;

        public OverlayTile previous;
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

        public void SetSprite(ArrowDirection d)
        {
            if (d == ArrowDirection.None)
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 0);
            else
            {
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = arrows[(int)d];
                GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }

    }
}
