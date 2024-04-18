using UnityEngine;

namespace LawlessGames.Tactics_Toolkit_Pathfinding.Pathfinding.Part_1___Grid_Setup.Scripts
{
    public class OverlayTile : MonoBehaviour
    {
        public int G;
        public int H;
        public int F => G + H;

        public bool isBlocked;

        public OverlayTile previous;
        public Vector3Int gridLocation;

        public void ShowTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public void HideTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        
        
    }
}
