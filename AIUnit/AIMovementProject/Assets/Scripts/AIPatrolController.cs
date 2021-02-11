using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolController : MonoBehaviour
{
    public Agent agent;
    public float speed = 3.0f;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float reachedThreshold = 0.5f;
    void Update()
    {
        Vector3 offset = waypoints[currentWaypointIndex].position - transform.position;
        offset.y = 0.0f;

        agent.velocity = offset.normalized * speed;
        agent.UpdateMovement(); //might not work

        //check if at location
        offset = waypoints[currentWaypointIndex].position - transform.position;
        if (offset.magnitude <= reachedThreshold) 
        {
            ++currentWaypointIndex;
            if (currentWaypointIndex >= waypoints.Length) 
            {
                currentWaypointIndex = 0;
            }
        }

    }
}
