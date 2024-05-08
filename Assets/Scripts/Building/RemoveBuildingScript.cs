using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Building
{
    [RequireComponent(typeof(Button))]
    public class RemoveBuildingScript : MonoBehaviour
    {
        [FormerlySerializedAs("itemButton")] public Button destroyButton;
        
        public Action EnteringDestroyMode;

        private void Start()
        {
            destroyButton = GetComponent<Button>();
            destroyButton.onClick.AddListener(TaskCuzButtonIsClicked);

            // TODO: Connect with BuildModeEvent to Display Button, and ExitBuildMode to not display.
            // TODO: Create DestroyMode code. : )
        }

        public void TaskCuzButtonIsClicked()
        {
            EnteringDestroyMode?.Invoke();
        }
    }
}
