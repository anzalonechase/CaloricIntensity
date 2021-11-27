using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Controller : MonoBehaviour
{

    public Transform fireSpawn;       // Set both in inspector        (This is where bullet comes from)
    public Rigidbody bullet;           //                              (This moves the bullet)



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
        

        if(Input.GetMouseButtonDown(0))
        {
            var bulletInstance = Instantiate(bullet, fireSpawn.position, fireSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

            bulletInstance.AddForce(fireSpawn.forward * force);         // Shoots out the bullet
        }

        if (Input.GetMouseButtonDown(0))
        {
            var bulletInstance = Instantiate(bullet, fireSpawn.position, fireSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

            bulletInstance.AddForce(fireSpawn.forward * force);         // Shoots out the bullet
        }


    }
}
