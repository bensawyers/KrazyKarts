using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Color colour;
    private List<Transform> waypoints = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = colour;
        Transform[] pathWaypoints = GetComponentsInChildren<Transform>();
        waypoints = new List<Transform>();

        for (int i = 0; i < pathWaypoints.Length; i++)
        {
            if(pathWaypoints[i] != transform)
            {
                waypoints.Add(pathWaypoints[i]);
            }
        }

        for(int i = 0; i < waypoints.Count; i++)
        {
            Vector3 currentWaypoint = waypoints[i].position;
            Vector3 prevWaypoint = Vector3.zero;

            if (i > 0)
            {
                prevWaypoint = waypoints[i - 1].position;
            } else if (i == 0 && waypoints.Count > 1)
            {
                prevWaypoint = waypoints[waypoints.Count - 1].position;
            }

            Gizmos.DrawLine(prevWaypoint, currentWaypoint);
            Gizmos.DrawWireCube(currentWaypoint, new Vector3(0.7f,0.7f,0.7f));
        }
    }
}
