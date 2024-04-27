using System;
using System.Collections.Generic;
using Building;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface__UI_
{
    public class UIController : MonoBehaviour
    {
        public Action<int> EnteringBuildMode; 
            public Action ExitBuildMode;
        public Button enterBuildModeButton, exitBuildModeButton;

        public Color outlineColor;
        [SerializeField] private List<Button> _itemButtonList;

        private void Start()
        {
            _itemButtonList = new List<Button>();

            foreach (var itemButton in gameObject.transform.GetComponentsInChildren<PurchasableItem>())
            {
                enterBuildModeButton.onClick.AddListener(() =>
                {
                    var tempInt = 1;
                    ResetButtonColor();
                    ModifyOutline(enterBuildModeButton);
                    EnteringBuildMode?.Invoke(tempInt);
                });
                _itemButtonList.Add(itemButton.itemButton);
            }
            
            exitBuildModeButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(exitBuildModeButton);
                ExitBuildMode?.Invoke();
            });
        }

        private void ModifyOutline(Button button)
        {
            var outline = button.GetComponent<Outline>();
            outline.effectColor = outlineColor;
            outline.enabled = true;
        }

        private void ResetButtonColor()
        {
            foreach (var button in _itemButtonList)
            {
                button.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
