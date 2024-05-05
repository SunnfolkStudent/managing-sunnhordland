using UnityEngine;
using UnityEngine.SceneManagement;

namespace User_Interface__UI_
{
    public class ChangeSceneExit : MonoBehaviour
    {
        public string scene;
    
        void Update()
        {
            SceneManager.LoadScene(scene);
        }
    }
}
