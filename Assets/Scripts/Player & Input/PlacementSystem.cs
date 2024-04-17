using System;
using UnityEngine;

namespace Player___Input
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private GameObject mouseIndicator;
        [SerializeField] private InputManager2 inputManager2;

        private void Update()
        {
            Vector3 mousePosition = inputManager2.GetSelectedMapPosition();
            mouseIndicator.transform.position = mousePosition;
        }
    }
}
