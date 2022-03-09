using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //draw a sphere below the player that will return true if it intersects with the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //if the player is grounded and velocity.y is less that 0 (i.e. moving downwards) set velocity back to -2
        if(isGrounded && velocity.y <= 0){
            velocity.y = -2f;
        } 

        //sprint increases the speed
        if(Input.GetKey("left shift")){
            speed = 4f;
        }
        else 
        {
            speed = 3f;
        }

        // get postional data based on mouse inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // get the current direction the player is facing
        Vector3 move = transform.right * x + transform.forward * z ;

        // move the player in that direction by a factor of thier speed and time
        controller.Move(move * speed * Time.deltaTime);


        // jumping mechanics 
        if(isGrounded && Input.GetButtonDown("Jump")){
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }

        // how fast is the player moving due to gravity
        velocity.y += gravity * Time.deltaTime;

        // move player according to gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
