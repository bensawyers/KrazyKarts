using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LapSystem : MonoBehaviour
{
    public Transform path;
    public Transform carParent;
    public GameObject player;
    public int totalLaps;

    private List<Transform> waypoints = new List<Transform>();
    private List<LapSystem> carLaps = new List<LapSystem>();
    private List<RaceCar> cars = new List<RaceCar>();
    private int currentWaypoint = 0;
    public int lapNum { get; protected set; }

    private static int WAYPOINT_VALUE = 100;
    private static int LAP_VALUE = 10000;

    // Start is called before the first frame update
    public void Start()
    {
        Transform[] pathWaypoints = path.GetComponentsInChildren<Transform>();
        waypoints = new List<Transform>();
        lapNum = 0;

        for (int i = 0; i < pathWaypoints.Length; i++)
        {
            if (pathWaypoints[i] != path.transform)
            {
                waypoints.Add(pathWaypoints[i]);
            }
        }

        LapSystem[] tempCars = carParent.GetComponentsInChildren<LapSystem>();
        LapSystem playerLap = player.GetComponent<LapSystem>();
        carLaps = new List<LapSystem>();

        for (int i = 0; i < tempCars.Length; i++)
        {
                carLaps.Add(tempCars[i]);
        }
        carLaps.Add(playerLap);

        RaceCar[] tempCars2 = carParent.GetComponentsInChildren<RaceCar>();
        RaceCar playerObj = player.GetComponent<RaceCar>();
        cars = new List<RaceCar>();

        for (int i = 0; i < tempCars.Length; i++)
        {
            cars.Add(tempCars2[i]);
        }
        cars.Add(playerObj);

    }

    public float getDistance()
    {
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        distance -= currentWaypoint * WAYPOINT_VALUE;
        distance -= lapNum * LAP_VALUE;
        return distance;
    }

    public void checkWaypointDistance()
    {
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distance < 10.0f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        }
    }

    public int getCarPosition()
    {
        float distance = getDistance();
        int position = 1;

        foreach (LapSystem car in getCars())
        {
            if (car.getDistance() < getDistance())
            {
                position++;
            }
        }

        return position;
    }

    public int getWaypoint()
    {
        return currentWaypoint;
    }

    public List<LapSystem> getCars()
    {
        return carLaps;
    }

    public void stopCars()
    {
        Debug.Log("Number of race cars: " + cars.Count);
        foreach (RaceCar car in cars)
        {
            car.SetCanMove(false);
            Debug.Log("canMove is: " + car.getCanMove());
        }
    }

    public abstract void UpdateLapNumber();
}
