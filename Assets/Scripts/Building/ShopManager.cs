using System.Collections.Generic;
using UnityEngine;
using User_Interface__UI_;

namespace Building
{
    // The ShopManager manages all purchases and sales, and gratitudeManagement.
    
    public class ShopManager : MonoBehaviour
    {
        private BuildManager _buildManager;
        private UIController _uiController;
        public static int ItemSelected;
        
        [SerializeField] private int currentGratitudePoints = 1000;

        [SerializeField] private BuildableObjectScrub[] itemScrubs;
        
        [SerializeField] private List<BuildableObjectScrub> buildingScrubs;
        [SerializeField] private List<BuildableObjectScrub> roadScrubs;
        [SerializeField] private List<BuildableObjectScrub> natureScrubs;
        
        // TODO: Create an automated method that fetches all buttons and puts them in a Dictionary.
        

        private void Start()
        {
            _uiController = FindFirstObjectByType<UIController>();
            _buildManager = FindFirstObjectByType<BuildManager>();
            
            foreach (var item in itemScrubs)
            {
                var itemType = item.itemType;
                switch (itemType)
                {
                    case TileType.Building:
                        buildingScrubs.Add(item);
                        continue;
                    case TileType.Nature:
                        natureScrubs.Add(item);
                        continue;
                    case TileType.Road:
                        roadScrubs.Add(item);
                        continue;
                    case TileType.Empty:
                        Debug.LogWarning("Whoops..." + item + " is TileType.Empty!");
                        continue;
                    default:
                        Debug.LogWarning("Whoops..." + item + " is not assigned a TileType!");
                        continue;
                }
            }
        }
        
        // The BuildManager will access this method
        public void BuyProduct(int itemIndex)
        {
            var selectedItem = itemScrubs[itemIndex];
            ItemSelected = selectedItem.itemIndex;

            if (CanWeAffordObject(selectedItem.itemPrice) >= 0)
            {
                currentGratitudePoints -= selectedItem.itemPrice;
            }
            else
            {
                Debug.Log("Not enough, you're missing: " + -CanWeAffordObject(selectedItem.itemPrice) + " gratitude points!");
            }

        }

        public void ProductSelectedForPlacing(int selectedItemIndex)
        {
            var selectedItem = itemScrubs[selectedItemIndex].itemIndex;
            _uiController.EnteringBuildMode(selectedItem);
        }

        public void SellProduct(int itemIndex)
        {
            
        }

        public int CanWeAffordObject(int itemCost)
        {
            return currentGratitudePoints - itemCost;
        }

        public int CurrentGratitudePoints()
        {
            return currentGratitudePoints;
        }

    }
}
