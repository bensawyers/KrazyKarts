using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour {

    public void ButtonHandlerBack()
    {
        StartCoroutine(LoadAsyncScene());
    }

    public void ButtonHandlerQuit()
    {
        StartCoroutine(LoadAsyncSceneMain());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Map1");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadAsyncSceneMain()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
