using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void ButtonHandlerPlay()
    {
        StartCoroutine(LoadAsyncScene());
    }
    
    public void ButtonHandlerSettings()
    {
        StartCoroutine(LoadAsyncSceneSet());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Map1");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadAsyncSceneSet()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SettingsMenu");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
