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

    void Start()
    {
        force = 1000f;               

    }

    void Update()
    {
        

        if(Input.GetMouseButtonDown(0))       //CLick left for sausage
        {
            var bulletInstance = Instantiate(hotDog, hotdogSpawn.position, hotdogSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

            bulletInstance.AddForce(hotdogSpawn.forward * force);         // Shoots out the bullet
        }

        if (Input.GetMouseButtonDown(1))   // CLick right for burger
        {
            var bulletInstance = Instantiate(burger, burgerSpawn.position, burgerSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

            bulletInstance.AddForce(burgerSpawn.forward * force);         // Shoots out the bullet
        }


    }
}
