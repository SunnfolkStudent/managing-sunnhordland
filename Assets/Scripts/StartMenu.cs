using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneChanger.SwitchToSingleScene("Main Game Scene");
    }

    public void GoToTitleScreen()
    {
        SceneChanger.SwitchToSingleScene("Title Screen");
    }

    public void GoToSettings()
    {
        SceneChanger.SwitchToSingleScene("Settings");
    }
    
    public void GoToCredits()
    {
        SceneChanger.SwitchToSingleScene("Credits");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        print("Quitting the Game.");
    }
}
