﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISteeringController : MonoBehaviour
{
    public Agent agent;

    [Header("Steering Settings")]
    public float maxSpeed = 3.0f;
    public float maxForce = 5.0f;
    [Header("List of behaviours")]
    List<SteeringBehavior> steerings = new List<SteeringBehavior>();
    [Header("Important Transforms")]
    public Transform seekTarget;
    
    protected Vector3 CalculateSteeringForce()
    {
        Vector3 steeringForce = Vector3.zero;
        for (int i = 0; i < steerings.Count; i++) 
        {
            steeringForce += steerings[i].Steer(this);//adds a value to the steering force based on the return of the method steer, since the steering behaviours will inherit have their own Steer method
        }
        //clamp it
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        return steeringForce;
    }
    private void Start() //I don't see a world where I will ever "call" the Start method since it will be used when the program starts
    {
        steerings.Add(new SeekSteering { target = seekTarget }); //in the start we'd add all the different behaviours
    }
    private void Update()//another method that auto activates and shouldn't be called by me
    {
        Vector3 steeringForce = CalculateSteeringForce();
        agent.velocity = Vector3.ClampMagnitude(agent.velocity + steeringForce, maxSpeed); //give em the clamps clamps
    }
}
public class SteeringBehavior
{
    public virtual Vector3 Steer(AISteeringController controller)
    {
        return Vector3.zero;
    }
}
public class SeekSteering : SteeringBehavior 
{
    public Transform target;
    public override Vector3 Steer(AISteeringController controller)
    {
        return (target.position - controller.transform.position).normalized * controller.maxSpeed;
    }
}
public class FleeSteering : SteeringBehavior 
{
    public Transform target;
    public override Vector3 Steer(AISteeringController controller)
    {
        return (target.position + controller.transform.position).normalized * controller.maxSpeed;
    }
}
public class WanderSteering : SteeringBehavior 
{
    public float distance = 8.0f;
    public float allowedRadius = 5f;
    public override Vector3 Steer(AISteeringController controller)
    {
        Vector3 offset = Random.onUnitSphere * allowedRadius;
        offset.y = 0.0f;
        offset += controller.transform.forward * distance;
        Vector3 offsetPosition = controller.transform.position + offset;
        return (offsetPosition - controller.transform.position).normalized * controller.maxSpeed;
    }
}
public class PursueSteering : SteeringBehavior 
{
    public Transform target;
    public override Vector3 Steer(AISteeringController controller)
    {
        return base.Steer(controller);
    }
}

