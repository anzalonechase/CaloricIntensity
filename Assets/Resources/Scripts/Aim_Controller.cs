using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Controller : MonoBehaviour
{

    public Transform fireSpawn;       // Set both in inspector        (This is where bullet comes from)
    public Rigidbody bullet;           //                              (This moves the bullet)

    public float force;       //WIll need to give it force

    // Start is called before the first frame update
    void Start()
    {
        force = 1000f;               

    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetMouseButtonDown(0))
        {
            var bulletInstance = Instantiate(bullet, fireSpawn.position, fireSpawn.rotation);        //creates an instance of bullet in fireSpawn positon

            bulletInstance.AddForce(fireSpawn.forward * force);         // Shoots out the bullet
        }

    }
}
