using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : GameInput
{
    public Transform path;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypoint = 0;
    private float Steer = 0f;
    private float Accel = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathWaypoints = path.GetComponentsInChildren<Transform>();
        waypoints = new List<Transform>();

        for (int i = 0; i < pathWaypoints.Length; i++)
        {
            if (pathWaypoints[i] != path.transform)
            {
                waypoints.Add(pathWaypoints[i]);
            }
        }
    }

    void SteerCar()
    {
        Vector3 distanceFromWaypoint = transform.InverseTransformPoint(waypoints[currentWaypoint].position);
        Steer = (distanceFromWaypoint.x / distanceFromWaypoint.magnitude);
    }

    void checkWaypointDistance()
    {
        if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 5.0f)
        {
            currentWaypoint = (currentWaypoint + 1)%waypoints.Count;
        }
    }

    void moveCar()
    {
        Vector3 fwdVec = transform.InverseTransformDirection(transform.forward);
        Accel = fwdVec.z;
    }

    public override Vector2 GetInputs()
    {
        SteerCar();
        moveCar();
        checkWaypointDistance();
        return new Vector2
        {
            x = Steer,
            y = Accel
        };
    }
}
