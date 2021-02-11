using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    //a reference to the rigidbody so that we can grab it and use it
    //(using the editor to assign)
    [SerializeField]
    protected Rigidbody rb3; //rigidbody 3d

    //velocity of the object it will define the object's position over time
    public virtual Vector3 velocity { get; set; }

    //movement method to update the object's position
    public virtual void UpdateMovement() 
    {
        //move rb3 to a new position
        //new position
        rb3.MovePosition(rb3.position + velocity * Time.deltaTime);
    }
}
