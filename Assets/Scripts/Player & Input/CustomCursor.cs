using System;
using Building;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player___Input
{
    public class CustomCursor : MonoBehaviour
    {
        private DestroyModeScript _destroyModeScript;
        // You must set the cursor in the inspector.
        public Texture2D customCursorDefault;
        public Texture2D customCursorDestroyMode;

        void Start()
        {
            _destroyModeScript = FindFirstObjectByType<DestroyModeScript>(FindObjectsInactive.Include);
            //set the cursor origin to its centre. (default is upper left corner)
            // Vector2 cursorOffset = new Vector2(customCursor.width/2, customCursor.height/2);
     
            //Sets the cursor to the Crosshair sprite with given offset 
            //and automatic switching to hardware default if necessary
            Cursor.SetCursor(customCursorDefault, default, CursorMode.Auto);
            _destroyModeScript.EnteringDestroyMode += ChangeCursor;
            
        }

        private void ChangeCursor()
        {
            Cursor.SetCursor(customCursorDestroyMode, default, CursorMode.Auto);
        }
        
    }
}
