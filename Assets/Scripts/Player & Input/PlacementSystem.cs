using System;
using UnityEngine;

namespace Player___Input
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private GameObject mouseIndicator, cellIndicator;
        [SerializeField] private InputManager2 inputManager2;
        [SerializeField] private Grid isometricGrid;

        private void Update()
        {
            Vector3 mousePosition = inputManager2.GetSelectedMapPosition();
            Vector3Int gridPosition = isometricGrid.WorldToCell(mousePosition);
            mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = isometricGrid.CellToWorld(gridPosition);
        }
    }
}
