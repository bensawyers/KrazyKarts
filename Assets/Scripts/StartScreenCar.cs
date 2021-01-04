using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenCar : MonoBehaviour
{
    Transform car;

    private void Start()
    {
        car = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        car.Rotate(0f, 1f, 0f, Space.Self);

    }
}
