using System;
using UnityEngine;
using UnityEngine.UI;
using User_Interface__UI_;

namespace Building
{
    [RequireComponent(typeof(Button))]
    public class PurchasableItem : MonoBehaviour
    {
        // private UIController _uiController;
        private ShopManager _shopManager;
        public BuildableObjectScrub itemScrub;
        public Button itemButton;
        
        public Action<int> EnteringBuildMode;

        private void Start()
        {
            _shopManager = FindFirstObjectByType<ShopManager>();
            Debug.Log("ProductIndex:" + itemScrub.itemIndex);
            gameObject.GetComponent<Image>().sprite = itemScrub.itemImage;
            itemButton = GetComponent<Button>();
            itemButton.onClick.AddListener(TaskCuzButtonIsClicked);
        }

        private void TaskCuzButtonIsClicked()
        {
            EnteringBuildMode?.Invoke(itemScrub.itemIndex);
        }

        public bool CanWeBuyProduct()
        {
            if (_shopManager.CanWeAffordObject(itemScrub.itemPrice) >= 0)
            {
                Debug.Log(itemScrub.itemIndex);
                _shopManager.ProductSelectedForPlacing(itemScrub.itemIndex);
                return true;
            }
            Debug.Log("Not enough, you're missing: " + -_shopManager.CanWeAffordObject(itemScrub.itemPrice) + " gratitude points!");
            return false;
        }
    }
}
