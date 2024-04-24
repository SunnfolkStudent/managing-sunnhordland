using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Testing_Scenes
{
    public class UIController : MonoBehaviour
    {
        public Action PlacingRoad, PlacingHouse, PlacingSpecial, PlacingBigStructure;
        public Button placeRoadButton, placeHouseButton, placeSpecialButton, placeBigStructureButton;

        public Color outlineColor;
        private List<Button> _buttonList;

        private void Start()
        {
            _buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton };
            
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
            
            placeBigStructureButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(placeBigStructureButton);
                PlacingBigStructure?.Invoke();
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
