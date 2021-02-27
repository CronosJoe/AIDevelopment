    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controllerComponent; //trying out this component
    [Header("Player values")]
    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;


    public Transform groundChecker;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded; //easiest approach I got
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f; //this sometimes will trigger a bit above ground so I'm hardcoding in -2 instead of reseting the velocity to 0 so the player will hit the ground
        }
        float x = Input.GetAxis("Horizontal"); //grabbing the x axis
        float z = Input.GetAxis("Vertical"); //grabbing the z axis

        Vector3 playerMovement = transform.right * x + transform.forward * z; //this will take into account direction the player is facing

        controllerComponent.Move(playerMovement * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controllerComponent.Move(velocity * Time.deltaTime);
    }
}
