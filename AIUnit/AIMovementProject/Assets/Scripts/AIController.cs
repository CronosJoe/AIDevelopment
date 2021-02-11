using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Agent agent;
    public float speed = 3.0f;

    public Transform target;

    private void Update()
    {
        Vector3 offset = target.position - transform.position;

        offset.y = 0.0f;

        agent.velocity = offset.normalized * speed; //updates the velocity
        agent.UpdateMovement();//moves the object
    }
}
