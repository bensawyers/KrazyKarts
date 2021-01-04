using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    Text CountdownText;
    float timeFloat = 3f;

    private void Start()
    {
        CountdownText = GetComponent<Text>();
    }

    private void Update()
    {
        timeFloat -= Time.deltaTime;
        int seconds = (int)timeFloat;

        if (seconds > 0)
        {          
            CountdownText.text = "" + seconds;
        } else if (seconds == 0)
        {
            CountdownText.text = "GO";
        }

        if(timeFloat < 0)
        {
            CountdownText.enabled = false;
        }
    }
}
