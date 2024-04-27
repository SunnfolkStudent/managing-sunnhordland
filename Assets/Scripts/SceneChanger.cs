using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static Coroutine CloseSceneCoroutine;
    private static string _test;

    private void Start()
    {
        CloseSceneCoroutine = StartCoroutine(CloseScene(_test));
    }

    public static void OpenShopUI()
    {
        SceneManager.LoadScene("Shop (UI)", LoadSceneMode.Additive);
    }
    
    public static void CloseShopUI()
    {
        SceneManager.UnloadSceneAsync("Shop (UI)");
    }
    
    // Keep in mind this unloads all other scenes.
    public static void SwitchToSingleScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public static void OpenAdditiveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public static IEnumerator CloseScene(string sceneName)
    {
        if (sceneName == _test)
        {
            Debug.Log("Ending Close Scene, due to test.");
            yield break;
        }
        var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        yield return new WaitUntil(() => asyncOperation.isDone);
        // TODO: Send a signal to whatever called upon this method, that the scene is closed.
            // Use the static CloseSceneCoroutine, and check if it is null.
    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void QuitGame()
    {
        Debug.Log("Exiting Game.");
        Application.Quit();
    }
}