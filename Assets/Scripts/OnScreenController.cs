using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnScreenController : MonoBehaviour {

    public void ButtonHandlerSettings()
    {
        StartCoroutine(LoadAsyncSceneSet());
    }

    IEnumerator LoadAsyncSceneSet()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("InGameMenu");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
