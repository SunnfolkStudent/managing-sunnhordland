using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    public class UIControllerSvs : MonoBehaviour
    {
        public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement, OnBigStructurePlacement;
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
                OnRoadPlacement?.Invoke();
            });
            
            placeHouseButton.onClick.AddListener(() => 
            {
                ResetButtonColor();
                ModifyOutline(placeHouseButton);
                OnHousePlacement?.Invoke();
            });
            
            placeSpecialButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(placeSpecialButton);
                OnSpecialPlacement?.Invoke();
            });
            
            placeBigStructureButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(placeBigStructureButton);
                OnBigStructurePlacement?.Invoke();
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
