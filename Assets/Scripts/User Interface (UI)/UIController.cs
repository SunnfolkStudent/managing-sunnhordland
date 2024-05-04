using System;
using System.Collections.Generic;
using Building;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface__UI_
{
    public class UIController : MonoBehaviour
    {
        private ShopManager _shopManager;
        
        public Action<int> EnteringBuildMode; 
        public Action ExitBuildMode;

        public Color outlineColor;
        [SerializeField] private List<Button> itemButtonList;
        [SerializeField] private List<Button> exitButtonList;

        private void Start()
        {
            itemButtonList = new List<Button>();
            exitButtonList = new List<Button>();

            // foreach (var shopList in GameObject.FindGameObjectsWithTag("Shop"))
            // {
            //     foreach (var uiElement in shopList.GetComponentsInChildren<RectTransform>())
            //     {
            //         uiElement.gameObject.SetActive(true);
            //     }
            //     shopList.SetActive(true);
            // }
            
            foreach (var item in gameObject.transform.GetComponentsInChildren<PurchasableItem>())
            {
                var itemButton = item.itemButton;
                itemButton.onClick.AddListener(() =>
                {
                    var itemInt = item.ProductIndex;
                    if (item.CanWeBuyProduct())
                    {
                        ResetButtonColor();
                        ModifyOutline(itemButton);
                        EnteringBuildMode?.Invoke(itemInt);
                    }
                });
                itemButtonList.Add(itemButton);
            }

            foreach (var exitButtonScript in gameObject.transform.GetComponentsInChildren<ExitButtonScript>())
            { 
                var exitButton = exitButtonScript.exitButton;
               exitButton.onClick.AddListener(() =>
               {
                   ResetButtonColor();
                   ModifyOutline(exitButton);
                   ExitBuildMode?.Invoke();
               });
                exitButtonList.Add(exitButton); 
            }

            // foreach (var objectToBeInactive in GameObject.FindGameObjectsWithTag("ShopInactive"))
            // {
            //     objectToBeInactive.SetActive(false);
            // }
        }

        private void ModifyOutline(Button button)
        {
            var outline = button.GetComponent<Outline>();
            outline.effectColor = outlineColor;
            outline.enabled = true;
        }

        private void ResetButtonColor()
        {
            foreach (var button in itemButtonList)
            {
                button.GetComponent<Outline>().enabled = false;
            }
        }

        public void BuyItemFromShop()
        {
            
        }
    }
}
