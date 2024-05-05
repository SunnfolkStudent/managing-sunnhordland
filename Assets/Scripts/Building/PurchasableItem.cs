using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
using User_Interface__UI_;

namespace Building
{
    [RequireComponent(typeof(Button))]
    public class PurchasableItem : MonoBehaviour
    {
        private UIController _uiController;
        private ShopManager _shopManager;
        [SerializeField] private BuildableObjectScrub itemScrub;
        public Button itemButton;
        
        [HideInInspector] internal int ProductIndex;

        private void Start()
        {
            _shopManager = FindFirstObjectByType<ShopManager>();
            ProductIndex = gameObject.GetComponent<PurchasableItem>().itemScrub.itemIndex;
            Debug.Log("ProductIndex:" + ProductIndex);
            gameObject.GetComponent<Image>().sprite = itemScrub.itemImage;
        }

        public bool CanWeBuyProduct()
        {
            if (_shopManager.CanWeAffordObject(itemScrub.itemPrice) >= 0)
            {
                Debug.Log(ProductIndex);
                _shopManager.ProductSelectedForPlacing(ProductIndex);
                return true;
            }
            Debug.Log("Not enough, you're missing: " + -_shopManager.CanWeAffordObject(itemScrub.itemPrice) + " gratitude points!");
            return false;
        }
    }
}
