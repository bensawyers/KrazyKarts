using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLeaderBoardMan : MonoBehaviour
{
    public Text FirstPosName;
    public Text SecondPosName;
    public Text ThirdPosName;
    public Text FirstPosTime;
    public Text SecondPosTime;
    public Text ThirdPosTime;

    // Start is called before the first frame update
    void Start()
    {
        float[] aiTimes = new float[2];

        float playerTime = PlayerPrefs.GetFloat("PlayerScore");

        aiTimes[0] = PlayerPrefs.GetFloat("Racer1Score");
        aiTimes[1] = PlayerPrefs.GetFloat("Racer2Score");

        int position = 1;

        foreach (float time in aiTimes)
        {
            if (playerTime > time)
            {
                position++;
            }
        }

        if (position == 1)
        {
            FirstPosName.text = "Player";
            float secondsFloat = playerTime;

            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            FirstPosTime.text = secondsWhole + ":" + milliSeconds;

            SecondPosName.text = "Racer 1";
            secondsFloat = aiTimes[0];

            secondsWhole = (int)secondsFloat;
            milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            secondsWhole += 3;
            milliSeconds = (int)milliSecondsFloat;
            SecondPosTime.text = secondsWhole + ":" + milliSeconds;

            ThirdPosName.text = "Racer 2";
            secondsFloat = aiTimes[1];

            secondsWhole = (int)secondsFloat;
            milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            secondsWhole += 4;
            milliSeconds = (int)milliSecondsFloat;
            ThirdPosTime.text = secondsWhole + ":" + milliSeconds;

        }
        else if (position == 2)
        {
            SecondPosName.text = "Player";
            float secondsFloat = playerTime;

            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            SecondPosTime.text = secondsWhole + ":" + milliSeconds;

            float firstAITime = aiTimes[0];
            int position2 = 1;
            for (int i = 1; i < aiTimes.Length; i++)
            {
                if (firstAITime > aiTimes[i])
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
                secondsFloat = aiTimes[1];

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                secondsWhole += 4;
                milliSeconds = (int)milliSecondsFloat;
                ThirdPosTime.text = secondsWhole + ":" + milliSeconds;
            }
            else
            {
                FirstPosName.text = "Racer 2";
                secondsFloat = aiTimes[1];

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                FirstPosTime.text = secondsWhole + ":" + milliSeconds;

                ThirdPosName.text = "Racer 1";
                secondsFloat = firstAITime;

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                secondsWhole += 4;
                milliSeconds = (int)milliSecondsFloat;
                ThirdPosTime.text = secondsWhole + ":" + milliSeconds;
            }
        }
        else
        {
            ThirdPosName.text = "Player";
            float secondsFloat = playerTime;

            int secondsWhole = (int)secondsFloat;
            float milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
            int milliSeconds = (int)milliSecondsFloat;
            ThirdPosTime.text = secondsWhole + ":" + milliSeconds;

            float firstAITime = aiTimes[0];
            int position2 = 1;
            for (int i = 1; i < aiTimes.Length; i++)
            {
                if (firstAITime > aiTimes[i])
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
                secondsFloat = aiTimes[1];

                secondsWhole = (int)secondsFloat;
                milliSecondsFloat = (secondsFloat - secondsWhole) * 100;
                milliSeconds = (int)milliSecondsFloat;
                SecondPosTime.text = secondsWhole + ":" + milliSeconds;
            }
            else
            {
                FirstPosName.text = "Racer 2";
                secondsFloat = aiTimes[1];

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
    }
}
