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

    public PathFindingBehavior pathFinder;
    
    protected Vector3 CalculateSteeringForce()
    {
        Vector3 steeringForce = Vector3.zero;
        float dist = Vector3.Distance(seekTarget.position, transform.position);
        //choosing our action
        if (dist < fleeSeekDist)
        {
            steeringForce += steerings[1].Steer(this, fleeSeekDist); //fleeing 
        }else if (dist > fleeSeekDist + 5.0f)
        {
            steeringForce += steerings[0].Steer(this, fleeSeekDist); //seeking
        }
        else
        {
            //this will be an attack / obstacle spawner

        }
        //clamp it
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        return steeringForce;
    }
    public bool checkObstacles(Vector3 direction) //gonna try and use the steering force as a direction
    {
        //I plan to use a raycast to check obstacles or walls in front of the enemy AI
        int layerMask = 1 << 9; //setting the layer mask so it will only collide with objects on layer 8

        RaycastHit hit; //setting our hit
        //these will also call a method that will adjust for the obstacle avoidance
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask)) //shooting the raycast
        { 
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow); //debugging tools
            Debug.Log("Did Hit" + direction);
            return true;
        }
        else
        {
            
            Debug.DrawRay(transform.position, direction * 1000, Color.white);
            Debug.Log("Did not Hit" + direction);
            return false;
        }
        //okay this works now I have to program something to move around the obstacle, maybe I should go back to the A* pathfinding to seek my way around the obstacle

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
        if (!checkObstacles(steeringForce))
        {
            agent.UpdateMovement();
        }
        else
        {
            //we're going to steer down the optimal tile path using seek with the target on every tile in the returned path list
        }
        
       
    }
}//Steering controller


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
        return (target.position - controller.transform.position).normalized * controller.maxSpeed;
    }
}
//going to need these two get pursue working
public class FleeSteering : SteeringBehavior 
{
    public Transform target;
    public override Vector3 Steer(AISteeringController controller,float fleeSeekDist)
    {
        return (controller.transform.position - target.position).normalized * controller.maxSpeed;
    }
}

