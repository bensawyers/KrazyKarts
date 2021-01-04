using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILapSystem : LapSystem
{
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

    }

    public override void UpdateLapNumber()
    {
        if (lapNum < totalLaps)
        {
            lapNum++;
        }
        else
        {
            raceFinished = true;
        }
    }
}
