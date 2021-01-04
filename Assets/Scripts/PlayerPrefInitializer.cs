using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("PlayerScore", 0);
        PlayerPrefs.SetFloat("Racer1Score", 0);
        PlayerPrefs.SetFloat("Racer2Score", 0);
    }
}
