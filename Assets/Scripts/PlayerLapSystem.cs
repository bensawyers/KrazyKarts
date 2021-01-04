using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLapSystem : LapSystem
{
    [Tooltip("Text object containing lap number.")]
    public Text LapText;
    [Tooltip("Text object containing the position")]
    public Text PositionText;
    public bool raceFinished { get; private set; }

    private new void Start()
    {
        raceFinished = false;
        base.Start();
    }

    public new float getDistance()
    {
        return base.getDistance();
    }

    public new void checkWaypointDistance()
    {
        base.checkWaypointDistance();
    }

    public new int getWaypoint()
    {
        return base.getWaypoint();
    }

    public new List<LapSystem> getCars()
    {
        return base.getCars();
    }

    public new int getCarPosition()
    {
        return base.getCarPosition();
    }

    public new void stopCars()
    {
        base.stopCars();
    }

    private void Update()
    {
        checkWaypointDistance();
        int position = getCarPosition();
        PositionText.text = getCarPosition() + "/" + getCars().Count;

    }

    public override void UpdateLapNumber()
    {
        lapNum++;
        LapText.text = "Lap " + lapNum;
        /*if (lapNum < totalLaps)
        {
            lapNum++;
            LapText.text = "Lap " + lapNum;
        } else
        {
            raceFinished = true;
        }*/
    }

    public void setRaceFinished(bool finished)
    {
        raceFinished = finished;
    }
}
