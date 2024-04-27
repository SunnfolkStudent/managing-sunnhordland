using System;
using UnityEngine;

namespace Building
{
    public class GratitudeDisplayer : MonoBehaviour
    {
        private ShopManager _shopManager;

        private int _gratitudePoints;
        // TODO: Add code to help display it in the UI.

        private void Update()
        {
            _gratitudePoints = _shopManager.CurrentGratitudePoints();
        }
    }
}
