using System;
using UnityEngine;
using UnityEngine.UI;

namespace Building
{
    public class PurchasableItem : MonoBehaviour
    {
        public ShopManager shopManager;
        public BuildableObjectScrub itemScrub;

        public Button itemButton;
        
        private int _productIndex;

        private void Awake()
        {
            _productIndex = itemScrub.itemIndex;
        }

        public void BuyProduct()
        {
            if (shopManager.CanWeAffordObject(itemScrub.itemPrice) >= 0)
            {
                shopManager.ProductSelectedForPlacing(_productIndex);
            }
            else
            {
                Debug.Log("Not enough, you're missing: " + -shopManager.CanWeAffordObject(itemScrub.itemPrice) + " gratitude points!");
            }
        }
    }
}
