using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    private float moveSpeed;        //was 10

    private float gravity;       // was 9.81
    private float jumpHeight;    // was 2.5        when character was 1 to 1 to 1

    private float yPosition; //We need this bc otherwise the jump would not work
                             // This is Because each "frame" it resets y position in a bad manner. We need to keep track of CURRENT
                             // is through each "frame"

    //Ammo
    public float burgerAmmo;
    public float hotdogAmmo;
    


    public CharacterController controller;      // Refrences the component in player component game obejct    (Set in inspector) 

  
    void Start()
    {
        //When the scene(game) starts give 3 burgers adn hotdogs as ammo
        moveSpeed = GameController.GameInstance.playerSpeed;
        gravity = 9.81f;
        jumpHeight = 2.5f;
        burgerAmmo = 12;                                                    //// change back to 3
        hotdogAmmo = 12;

    }


    void Update()
    {
        Debug.Log("Moving speed " +moveSpeed);
        moveSpeed = GameController.GameInstance.playerSpeed;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;   //calculates movement


        if (controller.isGrounded) //Pretty cool ocndition, basically if the "collider" in our player is touching something it isgrounded
        {

            if (Input.GetKeyDown(KeyCode.Space))     // If space bar is pressed then jump
            {
                yPosition = jumpHeight;      // Here we use the variable that keeps track of CURRENT y position which is the jump hegiht
            }

        }
        else
        {
            yPosition -= gravity * Time.deltaTime;   // applies gravity (each frame it goes down) BUT only if it is not grounded
        }
        move.y = yPosition;    // Makes sure to give CURRENT y position to the actual setter of the y position

                                                    
        controller.Move(move * moveSpeed * Time.deltaTime);   //Applies movement

    }


    public void OnControllerColliderHit(ControllerColliderHit collision)     // This is what charatcer controller uses to detect collisions
    {
        

        if (gameObject.tag == "Player" && collision.gameObject.tag == "Shop")  // If player is touching shop then reload ammo
        {
            GameController.GameInstance.GunHotDogAmount = 12;
            GameController.GameInstance.GunBurgerAmount = 12;
            burgerAmmo = 12;
            hotdogAmmo = 12;
        }
        else if (gameObject.tag == "Player" && collision.gameObject.tag == "Soda")   // If there is a collision with this tag the burger will destroy itself instantly
        {
            bool exists = false;
            for (int i = 0; i < GameController.GameInstance.itemList.Count; i++)
            {
                if (GameController.GameInstance.itemList[i].name == "Speedups")
                {
                    GameController.GameInstance.itemList[i].count++;
                    exists = true;

                }
            }
            if (!exists)
            {
                GameController.GameInstance.itemList.Add(new InventoryItem("Speedups", 1));
            }

            GameController.GameInstance.GainedSpeedUps++;
            Destroy(collision.gameObject);
        }

    }
}
