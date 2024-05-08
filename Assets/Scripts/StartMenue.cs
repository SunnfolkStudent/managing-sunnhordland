using UnityEngine;

public class StartMenue : MonoBehaviour
{
    public void StartGame()
    {
        SceneChanger.SwitchToSingleScene("Title Screen");
    }

    public void Settings()
    {
        
    }
    public void Credits()
    {
        SceneChanger.SwitchToSingleScene("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
