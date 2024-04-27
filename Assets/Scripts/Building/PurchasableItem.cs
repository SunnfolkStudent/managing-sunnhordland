using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using User_Interface__UI_;

namespace Building
{
    public class PurchasableItem : MonoBehaviour
    {
        private UIController _uiController;
        private ShopManager _shopManager;
        private BuildableObjectScrub _itemScrub;

        public Button itemButton;
        
        public int productIndex;

        private void Start()
        {
            productIndex = _itemScrub.itemIndex;
        }

        public bool CanWeBuyProduct()
        {
            if (_shopManager.CanWeAffordObject(_itemScrub.itemPrice) >= 0)
            {
                _shopManager.ProductSelectedForPlacing(productIndex);
                return true;
            }
            Debug.Log("Not enough, you're missing: " + -_shopManager.CanWeAffordObject(_itemScrub.itemPrice) + " gratitude points!");
            return false;
        }
    }
}
