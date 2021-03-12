using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISteeringController : MonoBehaviour
{
    public Agent agent;

    [Header("Steering Settings")]
    public float maxSpeed = 6.0f;
    public float maxForce = 10.0f;
    public float fleeSeekDist = 10.0f;
    [Header("List of behaviours")]
    List<SteeringBehavior> steerings = new List<SteeringBehavior>();
    [Header("Important Transforms")]
    public Transform seekTarget;
    
    protected Vector3 CalculateSteeringForce()
    {
        Vector3 steeringForce = Vector3.zero;
        for (int i = 0; i < steerings.Count; i++) 
        {
            steeringForce += steerings[i].Steer(this, fleeSeekDist);//adds a value to the steering force based on the return of the method steer, since the steering behaviours will inherit have their own Steer method
        }
        //clamp it
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        return steeringForce;
    }
    private void Start() //I don't see a world where I will ever "call" the Start method since it will be used when the program starts
    {
        steerings.Add(new SeekSteering { target = seekTarget }); //in the start we'd add all the different behaviours
        steerings.Add(new FleeSteering { target = seekTarget });
    }
    private void Update()//another method that auto activates and shouldn't be called by me
    {
        Vector3 steeringForce = CalculateSteeringForce();
        agent.velocity = Vector3.ClampMagnitude(agent.velocity + steeringForce, maxSpeed); //give em the clamps clamps
        agent.UpdateMovement();
    }
}
public class SteeringBehavior
{
    public virtual Vector3 Steer(AISteeringController controller, float fleeSeekDist)
    {
        return Vector3.zero;
    }
}
public class SeekSteering : SteeringBehavior 
{
    public Transform target;
    public override Vector3 Steer(AISteeringController controller, float fleeSeekDist)
    {
        float dist = Vector3.Distance(target.position, controller.transform.position);
        if (dist >= fleeSeekDist)
        {
            return (target.position - controller.transform.position).normalized * controller.maxSpeed;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
//going to need these two get pursue working
public class FleeSteering : SteeringBehavior 
{
    public Transform target;
    public override Vector3 Steer(AISteeringController controller,float fleeSeekDist)
    {
        float dist = Vector3.Distance(target.position, controller.transform.position);
        if (dist < fleeSeekDist)
        {
            return (target.position + controller.transform.position).normalized * controller.maxSpeed;
        }
        else 
        {
            return Vector3.zero; //since I am adding them together when I calculate movement this should work fine 
        }
    }
}

