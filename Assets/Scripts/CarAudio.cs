using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource idle;
    public AudioSource running;
    RaceCar car;

    void Awake()
    {
        car = GetComponentInParent<RaceCar>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0f;

        if (car != null)
        {
            speed = car.LocalSpeed();
        }

        running.volume = Mathf.Lerp(.1f, 1, speed / 2);
        running.pitch = Mathf.Lerp(.8f, 1.4f, speed / 4 + Mathf.Sin(Time.time) * .1f);
        idle.volume = Mathf.Lerp(.5f, 0, speed);
    }
}
