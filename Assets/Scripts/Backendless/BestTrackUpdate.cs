using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTrackUpdate : MonoBehaviour
{
    public Text TimeText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoteLapTimeManager.Instance.GetLapTimeCR(UpdateText));
    }

    void UpdateText(double time)
    {
        double tempTime = time;
        int secondsWhole = (int)time;
        double milliSecondsDouble = (tempTime - secondsWhole) * 100;
        int milliSeconds = (int)milliSecondsDouble;

        TimeText.text = secondsWhole + ":" + milliSeconds;
    }
}
