using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public Text TimerText;
    public PlayerLapSystem LapSystem;
    float secondsFloat = 0f;
    bool raceStarted = false;

    private void Start()
    {
        StartCoroutine(waitForCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!LapSystem.raceFinished && raceStarted)
        {
            secondsFloat += Time.deltaTime;
            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            TimerText.text = secondsWhole + ":" + milliSeconds;
        }
    }

    public float GetTime()
    {
        return secondsFloat;
    }

    IEnumerator waitForCountdown()
    {
        yield return new WaitForSeconds(2.5f);
        raceStarted = true;
    }
}
