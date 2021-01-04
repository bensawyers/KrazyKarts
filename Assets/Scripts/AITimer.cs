using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITimer : MonoBehaviour
{
    public AILapSystem LapSystem;
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
