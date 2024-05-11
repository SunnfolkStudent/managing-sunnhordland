using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Building
{
    [RequireComponent(typeof(Button))]
    public class DestroyModeScript : MonoBehaviour
    {
        public Button destroyButton;
        private Image _buttonImage;
        
        public Action EnteringDestroyMode;
        public Action ExitDestroyMode;

        private void Start()
        {
            destroyButton = GetComponent<Button>();
            _buttonImage = GetComponent<Image>();
            destroyButton.onClick.AddListener(TaskCuzButtonIsClicked);

            // TODO: Connect with BuildModeEvent to Display Button, and ExitBuildMode to not display.
            // TODO: Create DestroyMode code. : )
        }

        private void Update()
        {
            if (BuildManager.InBuildMode || BuildManager.InDestroyMode)
            {
                _buttonImage.color = new Color(255, 255, 255, 1);
            }
            else
            {
                _buttonImage.color = new Color(255, 255, 255, 0);
            }
        }

        public void TaskCuzButtonIsClicked()
        {
            if (!BuildManager.InDestroyMode)
            {
                EnteringDestroyMode?.Invoke();
            }
            else
            {
                ExitDestroyMode?.Invoke();
            }
        }
    }
}
