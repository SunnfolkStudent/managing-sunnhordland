using System.Linq;
using Building;
using UnityEngine;

namespace Player___Input
{
    public class PlayerInput : MonoBehaviour
    {
        // TODO: Make PlayerInput only check and send mouseInputs, and indicate position of mouseClick.
        
        public GameObject cursor;
       
        // TODO: Remove temp character and replace with build marker

        private BuildableObjectScrub _buildableObjectScrub;
        private GameObject BuildingPrefab => _buildableObjectScrub.itemObject;
        public GameObject positionMarkerPrefab;
        private BuildingInfo _positionMarker;

        // TODO: Add RoadBuilder with roadTranslator.
        // TODO: Add BuildingManager, for placing buildings.

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (hit.HasValue)
            {
                OverlayIsoTile overlayIsoTile = hit.Value.collider.gameObject.GetComponent<OverlayIsoTile>();
                cursor.transform.position = overlayIsoTile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayIsoTile.transform.GetComponent<SpriteRenderer>().sortingOrder;

                if (Input.GetMouseButtonDown(0))
                {
                    overlayIsoTile.ShowTile();
                    Debug.Log(overlayIsoTile.gameObject.name);

                    if (_positionMarker == null)
                    {
                        // This creates a marker that shows that we are building here.
                        _positionMarker = Instantiate(positionMarkerPrefab).GetComponent<BuildingInfo>();
                        PositionMarkerOnLine(overlayIsoTile);
                    }
                    else
                    {
                        overlayIsoTile.gameObject.GetComponent<OverlayIsoTile>().HideTile();
                    }
                }
            }
        }

        private void PositionMarkerOnLine(OverlayIsoTile tile)
        {
            var position = tile.transform.position;
            _positionMarker.transform.position = new Vector3(position.x, position.y + 0.0001f, position.z);
            _positionMarker.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            _positionMarker.standingOnTile = tile;
        }

        // The "?" after RaycastHit2D means that RaycastHit2D is Nullable
        // It means it can return either a Vector2 from Raycast2D or null.
        private static RaycastHit2D? GetFocusedOnTile()
        {
            // ReSharper disable once PossibleNullReferenceException
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Changing to Vector2, cuz we 2D isometric, not 3D.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Using RaycastAll with a list, cuz it's more reliable in finding the right tile
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }

            return null;
        }
    }
}

