using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollisionDetection : MonoBehaviour
{
    public LapSystem lapSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("StartLine"))
        {
            lapSystem.UpdateLapNumber();
        }
    }
}
