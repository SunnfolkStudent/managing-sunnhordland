using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface__UI_
{
    public class UIController : MonoBehaviour
    {
        public Action PlacingRoad, PlacingHouse, PlacingSpecial, ExitBuildMode;
        public Button placeRoadButton, placeHouseButton, placeSpecialButton, exitBuildModeButton;

        public Color outlineColor;
        private List<Button> _buttonList;

        private void Start()
        {
            _buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton, exitBuildModeButton };
            
            placeRoadButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(placeRoadButton);
                PlacingRoad?.Invoke();
            });
            
            placeHouseButton.onClick.AddListener(() => 
            {
                ResetButtonColor();
                ModifyOutline(placeHouseButton);
                PlacingHouse?.Invoke();
            });
            
            placeSpecialButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(placeSpecialButton);
                PlacingSpecial?.Invoke();
            });
            
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
            foreach (var button in _buttonList)
            {
                button.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
