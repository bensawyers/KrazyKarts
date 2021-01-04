using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiAnimation : MonoBehaviour
{
    public PlayerLapSystem player;
    ParticleSystem confetti;
    bool playAnim = true;

    // Start is called before the first frame update
    void Start()
    {
        confetti = GetComponent<ParticleSystem>();
        confetti.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.raceFinished)
        {
            if (playAnim)
            {
                confetti.Play();
                playAnim = false;
            }          
        }
        
    }
}
