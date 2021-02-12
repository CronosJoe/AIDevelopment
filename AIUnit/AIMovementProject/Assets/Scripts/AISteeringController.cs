using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISteeringController : MonoBehaviour
{
    public Agent agent;

    [Header("Steering Settings")]
    public float maxSpeed = 3.0f;
    public float maxForce = 5.0f;
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
