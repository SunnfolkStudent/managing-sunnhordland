using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scenemanagement;

public class ChangeScene-Exit : MonoBehaviour
{
public string Scene;
    
    void Update()
    {
        SceneManager.LoadScene("MyScene");
    }
}
