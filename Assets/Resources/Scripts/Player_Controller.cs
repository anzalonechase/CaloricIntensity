using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public float moveSpeed = 10f;

    public float gravity = 9.81f;
    public float jumpHeight = 2.5f;

    public float yPosition; //We need this bc otherwise the jump would not work
                            // This is Because each "frame" it resets y position in a bad manner. We need to keep track of CURRENT
                            // is through each "frame"

    public CharacterController controller;      // Refrences the component in player component game obejct    (Set in inspector) 

  
    void Start()
    {
        
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;   //calculates movement





        if(controller.isGrounded) //Pretty cool ocndition, basically if the "collider" in our player is touching something it isgrounded
        {

            if(Input.GetKeyDown(KeyCode.Space))     // If space bar is pressed then jump
            {
                yPosition = jumpHeight;      // Here we use the variable that keeps track of CURRENT y position which is the jump hegiht
            }

        }


        yPosition -= gravity * Time.deltaTime;   // applies gravity (each frame it goes down) 

        move.y = yPosition;    // Makes sure to give CURRENT y position to the actual setter of the y position

                                    





        controller.Move(move * moveSpeed * Time.deltaTime);   //Applies movement

    }
}
