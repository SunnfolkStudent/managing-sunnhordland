using UnityEngine;

namespace Building
{
    [CreateAssetMenu]
    public class BuildableObjectScrub : ScriptableObject
    {
        public GameObject itemObject;
        public string itemName;
        public string itemDescription;
        public Sprite itemImage;
        public Vector2Int itemTileSize;
        public int itemPrice;
    }
}
