using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Controller : MonoBehaviour
{

    public Transform burgerSpawn;       // Set both in inspector        (This is where burger comes from)
    public Rigidbody burger;           //                              (This moves the burger)

    public Transform hotdogSpawn;       // Set both in inspector        (This is where hotdog comes from)
    public Rigidbody hotDog;           //                              (This moves the hotdog)



    public float force;       //WIll need to give it force


    Player_Controller playerScript;   //Refrence the player script to get the ammo number


    void Start()
    {
        force = 1000f;

        playerScript = GameObject.Find("Player").GetComponent<Player_Controller>();     // gets reference to player script to access ammo count
         
    }

    void Update()
    {

        if (GameController.GameInstance.numberOfCustomers > 0 && GameController.GameInstance.gameTime > 0)
        {


            if (Input.GetMouseButtonDown(0))       //CLick left for sausage
            {
                if (GameController.GameInstance.GunHotDogAmount > 0)
                {
                    GameController.GameInstance.GunHotDogAmount--;
                }
               
                if (playerScript.hotdogAmmo > 0)  // Checks if it has ammo
                {
                    var bulletInstance = Instantiate(hotDog, hotdogSpawn.position, hotdogSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

                    bulletInstance.AddForce(hotdogSpawn.forward * force);         // Shoots out the bullet

                    playerScript.hotdogAmmo -= 1;  // Decrements hotdog ammo bc you just used a shot


                    Debug.Log("Hot dog ammo:     " + playerScript.hotdogAmmo);
                }
                else
                {
                    Debug.Log("No hotdog ammo left");
                    // Lets the player know there is not enoguh hotdog ammo 
                }

            }




            if (Input.GetMouseButtonDown(1))   // CLick right for burger
            {
                if (GameController.GameInstance.GunBurgerAmount > 0)
                {
                    GameController.GameInstance.GunBurgerAmount--;
                }
                if (playerScript.burgerAmmo > 0)
                {

                    var bulletInstance = Instantiate(burger, burgerSpawn.position, burgerSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

                    bulletInstance.AddForce(burgerSpawn.forward * force);         // Shoots out the bullet

                    playerScript.burgerAmmo -= 1; // Decrement the burger ammo bc you just used a shot

                    Debug.Log("Burger ammo:     " + playerScript.burgerAmmo);
                }
                else
                {
                    Debug.Log("No burger ammo left");
                    // Lest display message saying player has no burger ammo left
                }


            }
        }


    }

















}
