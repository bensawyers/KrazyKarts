using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        float secondsFloat = PlayerPrefs.GetFloat("PlayerScore");

        int secondsWhole = (int)secondsFloat;
        float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
        int milliSeconds = (int)milliSecondsFloat;
        TimeText.text = secondsWhole + ":" + milliSeconds;
    }
}
