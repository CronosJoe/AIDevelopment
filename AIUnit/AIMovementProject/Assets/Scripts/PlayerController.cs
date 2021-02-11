using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Agent agent; //get access to the agent script
    public float speed = 3.0f;//basic default speed
    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);
        agent.velocity = input * speed;
        agent.UpdateMovement();
    }
}
