using System;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface__UI_
{
    [RequireComponent(typeof(Button))]
    public class ExitButtonScript : MonoBehaviour
    {
        public Button exitButton;

        private void Start()
        {
            if (TryGetComponent(out Button button))
            {
                exitButton = button;
            }
            else
            {
                Debug.LogWarning(gameObject + "This gameObject w/Script hasn't been assigned a button!");
            }
        }
    }
}
