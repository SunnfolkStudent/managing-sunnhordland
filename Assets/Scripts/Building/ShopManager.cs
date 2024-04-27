using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    // The ShopManager manages all purchases and sales, and gratitudeManagement.
    
    public class ShopManager : MonoBehaviour
    {
        private BuildManager _buildManager;
        [SerializeField] private int currentGratitudePoints = 1000;

        [SerializeField] private BuildableObjectScrub[] itemScrubs;

        private List<BuildableObjectScrub>[] _buildingScrubs;
        private List<BuildableObjectScrub>[] _roadScrubs;
        private List<BuildableObjectScrub>[] _natureScrubs;
        
        // TODO: Create an automated method that fetches all buttons and puts them in a Dictionary.

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            foreach (var item in itemScrubs)
            {
                
            }
        }
        
        // The BuildManager will access this method
        public void BuyProduct(int itemIndex)
        {
            int selectedProduct = itemIndex;
            // TODO: Retrieve stored scrub from own list / array through given index.
        }

        public void ProductSelectedForPlacing(int itemIndex)
        {
            
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
