using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerLapSystem lapSystem;
    public GameObject scoreboard;
    public AudioSource idle;
    public AudioSource running;
    public AudioSource winningSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("StartLine"))
        {
            if(lapSystem.lapNum < lapSystem.totalLaps)
            {
                lapSystem.UpdateLapNumber();
            } else
            {
                lapSystem.setRaceFinished(true);
                winningSound.Play();
                StartCoroutine(Wait());

            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        Time.timeScale = 0f;
        idle.mute = !idle.mute;
        running.mute = !running.mute;
        scoreboard.SetActive(true);
    }
}
