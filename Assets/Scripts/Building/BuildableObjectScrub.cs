using UnityEngine;

namespace Building
{
    [CreateAssetMenu]
    public class BuildableObjectScrub : ScriptableObject
    {
        // Related to Object and TileMap
        public int itemIndex;
        public GameObject itemObject;
        public GameObject itemObjectRotated;
        public int itemTileSizeX;
        public int itemTileSizeY;
        public TileType itemType;
        
        // Related to Shop
        public string itemName;
        public string itemDescription;
        public Sprite itemImage;
        public int itemPrice;
    }
    
}
