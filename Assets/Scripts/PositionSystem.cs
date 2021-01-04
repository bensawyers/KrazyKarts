using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionSystem : MonoBehaviour
{
    public int position;
    public int NumberOfVehicles;
    public Text positionText;

    private void Start()
    {
        positionText.text = position + "/" + NumberOfVehicles;
    }

    public void UpdatePosition(int newPos)
    {
        position = newPos;
        positionText.text = position + "/" + NumberOfVehicles;
    }
}
