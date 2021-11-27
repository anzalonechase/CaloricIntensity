using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public float moveSpeed = 10f;



    public CharacterController controller;      // Set in inspector 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;   //calculates movement

        controller.Move(move * moveSpeed * Time.deltaTime);   //Applies movement

    }
}
