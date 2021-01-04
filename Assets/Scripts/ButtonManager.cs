using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
   public AudioSource BackgroundSound;

   public void EnterPauseMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void ExitPauseMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void MoveToNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseMusic()
    {
        BackgroundSound.mute = !BackgroundSound.mute;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
