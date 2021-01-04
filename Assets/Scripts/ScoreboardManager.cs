using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public PlayerTimer Player;
    public Transform AIParent;
    public Text FirstPosName;
    public Text SecondPosName;
    public Text ThirdPosName;
    public Text FirstPosTime;
    public Text SecondPosTime;
    public Text ThirdPosTime;

    List<AITimer> aiTimes;

    // Start is called before the first frame update
    void Start()
    {
        AITimer[] tempTimes = AIParent.GetComponentsInChildren<AITimer>();
        aiTimes = new List<AITimer>();
        for(int i = 0; i < tempTimes.Length; i++)
        {
            aiTimes.Add(tempTimes[i]);
        }

        int position = 1;

        foreach (AITimer time in aiTimes)
        {
            if (Player.GetTime() > time.GetTime())
            {
                position++;
            }
        }

        if (position == 1)
        {
            FirstPosName.text = "Player";
            float secondsFloat = Player.GetTime();

            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            FirstPosTime.text = secondsWhole + ":" + milliSeconds;

            SecondPosName.text = "Racer 1";
            secondsFloat = aiTimes[0].GetTime();

            secondsWhole = (int)secondsFloat;
            milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            secondsWhole += 2;
            milliSeconds = (int)milliSecondsFloat;
            SecondPosTime.text = secondsWhole + ":" + milliSeconds;

            ThirdPosName.text = "Racer 2";
            secondsFloat = aiTimes[1].GetTime();

            secondsWhole = (int)secondsFloat;
            milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            secondsWhole += 3;
            milliSeconds = (int)milliSecondsFloat;
            ThirdPosTime.text = secondsWhole + ":" + milliSeconds;

        }
        else if (position == 2)
        {
            SecondPosName.text = "Player";
            float secondsFloat = Player.GetTime();

            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            SecondPosTime.text = secondsWhole + ":" + milliSeconds;

            float firstAITime = aiTimes[0].GetTime();
            int position2 = 1;
            for (int i = 1; i < aiTimes.Count; i++)
            {
                if (firstAITime > aiTimes[i].GetTime())
                {
                    position2 += 2;
                }
            }

            if (position2 == 1)
            {
                FirstPosName.text = "Racer 1";
                secondsFloat = firstAITime;

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                FirstPosTime.text = secondsWhole + ":" + milliSeconds;

                ThirdPosName.text = "Racer 2";
                secondsFloat = aiTimes[1].GetTime();

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                secondsWhole += 3;
                milliSeconds = (int)milliSecondsFloat;
                ThirdPosTime.text = secondsWhole + ":" + milliSeconds;
            }
            else
            {
                FirstPosName.text = "Racer 2";
                secondsFloat = aiTimes[1].GetTime();

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                FirstPosTime.text = secondsWhole + ":" + milliSeconds;

                ThirdPosName.text = "Racer 1";
                secondsFloat = firstAITime;

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                secondsWhole += 3;
                milliSeconds = (int)milliSecondsFloat;
                ThirdPosTime.text = secondsWhole + ":" + milliSeconds;
            }
        }
        else
        {
            ThirdPosName.text = "Player";
            float secondsFloat = Player.GetTime();

            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            ThirdPosTime.text = secondsWhole + ":" + milliSeconds;

            float firstAITime = aiTimes[0].GetTime();
            int position2 = 1;
            for (int i = 1; i < aiTimes.Count; i++)
            {
                if (firstAITime > aiTimes[i].GetTime())
                {
                    position2++;
                }
            }

            if (position2 == 1)
            {
                FirstPosName.text = "Racer 1";
                secondsFloat = firstAITime;

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                FirstPosTime.text = secondsWhole + ":" + milliSeconds;

                SecondPosName.text = "Racer 2";
                secondsFloat = aiTimes[1].GetTime();

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                SecondPosTime.text = secondsWhole + ":" + milliSeconds;
            }
            else
            {
                FirstPosName.text = "Racer 2";
                secondsFloat = aiTimes[1].GetTime();

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                FirstPosTime.text = secondsWhole + ":" + milliSeconds;

                SecondPosName.text = "Racer 1";
                secondsFloat = firstAITime;

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                SecondPosTime.text = secondsWhole + ":" + milliSeconds;
            }
        }

        PlayerPrefs.SetFloat("PlayerScore", PlayerPrefs.GetFloat("PlayerScore") + Player.GetTime());
        PlayerPrefs.SetFloat("Racer1Score", PlayerPrefs.GetFloat("Racer1Score") + aiTimes[0].GetTime());
        PlayerPrefs.SetFloat("Racer2Score", PlayerPrefs.GetFloat("Racer2Score") + aiTimes[1].GetTime());

        StartCoroutine(RemoteLapTimeManager.Instance.GetLapTimeCR(UpdateBest));
    }

    void BackendlessComplete()
    {
        Debug.Log("Set in DB");
    }

    void UpdateBest(double time)
    {
        if((double)Player.GetTime() < time)
        {
            StartCoroutine(RemoteLapTimeManager.Instance.SetLapTimeCR((double)Player.GetTime(), BackendlessComplete));
        }
    }
}
